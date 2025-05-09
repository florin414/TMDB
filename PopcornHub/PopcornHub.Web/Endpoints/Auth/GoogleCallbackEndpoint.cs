using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.IServices;

namespace PopcornHub.Web.Endpoints.Auth
{
    public class GoogleCallbackEndpoint : EndpointWithoutRequest
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public GoogleCallbackEndpoint(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public override void Configure()
        {
            Get("/google/callback");
            Group<AuthGroup>();
            AllowAnonymous();
        }
        

        public override async Task HandleAsync(CancellationToken ct)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var user = await GetOrCreateUserFromGoogleAsync(authenticateResult.Principal);

            // Autentifică utilizatorul
            var signInResult = await _signInManager.PasswordSignInAsync(user, "dummyPassword", false, false);
            if (!signInResult.Succeeded)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            // Generăm tokenurile folosind TokenService
            var accessToken = await _tokenService.GenerateJwtTokenAsync(user);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

            // Returnează tokenurile
            await SendAsync(new
            {
                accessToken,
                refreshToken
            });
        }

        private async Task<User> GetOrCreateUserFromGoogleAsync(ClaimsPrincipal principal)
        {
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email not found in Google response");

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return user;

            user = new User { Email = email, UserName = email };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            var providerKey = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(providerKey))
            {
                var loginInfo = new UserLoginInfo("Google", providerKey, "Google");
                await _userManager.AddLoginAsync(user, loginInfo);
            }

            return user;
        }
    }
}
