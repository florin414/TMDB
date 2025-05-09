using PopcornHub.Domain.DTOs.Login;
using PopcornHub.Domain.IServices;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace PopcornHub.Web.Endpoints.Auth;

public class GoogleLoginEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/google/login");
        Group<AuthGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var redirectUrl = "/auth/google/callback";
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        });
    }
}