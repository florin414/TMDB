using CSharpFunctionalExtensions;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Auth;
using PopcornHub.Web.DomainMappings.Auth;
using PopcornHub.Web.Validators;

namespace PopcornHub.Web.Endpoints.Auth;

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IAuthService _authService;

    public LoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void OnBeforeValidate(LoginRequest req)
    {
        LoginRequestValidator.Validate(req, AddError);
    }
    
    public override void Configure()
    {
        Post("/login");
        Group<AuthGroup>();
        AllowAnonymous();

        Summary(s =>
        {
            s.Summary = "Authenticate and log in the user.";
            s.Description = "Logs in the user with the provided email and password and returns a JWT token for authenticated sessions.";
            s.Response(StatusCodes.Status200OK, "Successfully logged in.");
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Invalid credentials.");
        });
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(request.ToModel());
        
        await result.Match(
            async () => await SendOkAsync(ct),
            async _ => await SendErrorsAsync(StatusCodes.Status400BadRequest, ct)
        );
    }
}