using FastEndpoints;
using PopcornHub.Domain.DTOs.ApiError;

namespace PopcornHub.Web.Endpoints.Auth;

public class CheckEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/check");
        Group<AuthGroup>();

        Summary(s =>
        {
            s.Summary = "Check if the user is authenticated.";
            s.Description = "This endpoint checks if the user is authenticated by verifying the presence and validity of the access token in the request's cookies. If the token is valid, the user is considered logged in.";
            s.Response<ApiErrorResponse>(StatusCodes.Status200OK, "User is authenticated.");
            s.Response<ApiErrorResponse>(StatusCodes.Status401Unauthorized, "User is not authenticated or the token is invalid.");
        });
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}