using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using PopcornHub.Domain.Entities;
using ITokenService = PopcornHub.Domain.IServices.ITokenService;

namespace PopcornHub.Application.Services;

public class TokenService : ITokenService
{
    private readonly IIdentityServerInteractionService _identityServerInteractionService;
    private readonly ITokenCreationService _tokenCreationService;  // Pentru generarea tokenurilor
    private const string AccessToken = "access_token";
    private const string RefreshToken = "refresh_token";

    public TokenService(IIdentityServerInteractionService identityServerInteractionService, ITokenCreationService tokenCreationService)
    {
        _identityServerInteractionService = identityServerInteractionService;
        _tokenCreationService = tokenCreationService;
    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        // Generăm un JWT token folosind IdentityServer sau un serviciu intern.
        var accessTokenResult = await _tokenCreationService.CreateTokenAsync(GenerateToken(user, AccessToken));
        return accessTokenResult;
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        // Generăm refresh token folosind IdentityServer sau un serviciu intern.
        var accessTokenResult = await _tokenCreationService.CreateTokenAsync(GenerateToken(user, RefreshToken));
        return accessTokenResult;
    }

    private Token GenerateToken(User user, string tokenTypes)
    {
        // Creăm un obiect Token cu detalii despre utilizator, client și scope
        var token = new Token
        {
            ClientId = "google-client",  // ID-ul clientului configurat în IdentityServer
            Issuer = "PopcornHub",  // Numele sau ID-ul issuer-ului
            Lifetime = tokenTypes == AccessToken ? 3600 : 36000,  // Durata de viață a tokenului în secunde (1 oră)
            Claims = new HashSet<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.Email, user.Email)
            },
            Type = tokenTypes,
            IncludeJwtId = true  // Include un ID unic pentru token
        };
        return token;
    }
}