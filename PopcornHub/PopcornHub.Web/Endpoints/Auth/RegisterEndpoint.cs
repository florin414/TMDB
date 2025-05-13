using CSharpFunctionalExtensions;
using FastEndpoints;
using PopcornHub.Application.IServices;
using PopcornHub.Domain.DTOs.ApiError;
using PopcornHub.Domain.DTOs.Auth;
using PopcornHub.Web.DomainMappings.Auth;
using PopcornHub.Web.Validators;

namespace PopcornHub.Web.Endpoints.Auth;

public class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly IAuthService _authService;

    public RegisterEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void OnBeforeValidate(RegisterRequest req)
    {
        RegisterRequestValidator.Validate(req, AddError);
    }
    
    public override void Configure()
    {
        Post("/register");
        Group<AuthGroup>();
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Register a new user account.";
            s.Description = "Creates a new user account by validating and storing the provided username, email, and password.";
            s.Response(StatusCodes.Status200OK, "User registered successfully.");
            s.Response<ApiErrorResponse>(StatusCodes.Status400BadRequest, "Username or email already exists.");
        });
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        var result = await _authService.RegisterAsync(request.ToModel());

        await result.Match(
            async () => await SendOkAsync(ct),
            async _ => await SendErrorsAsync(StatusCodes.Status400BadRequest, ct)
        );
    }
}


