using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using PopcornHub.Application.IServices;
using PopcornHub.Application.MovieApiClient;
using PopcornHub.Application.Services;
using PopcornHub.Domain;
using PopcornHub.Domain.IRepositories;
using PopcornHub.Domain.IServices;
using PopcornHub.Infrastructure.Context;
using PopcornHub.Infrastructure.DomainServices;
using PopcornHub.Infrastructure.Repositories;
using PopcornHub.Shared.Configurations;
using PopcornHub.Shared.Configurations.Settings;
using PopcornHub.Shared.Constants;
using PopcornHub.Shared.Utils;
using PopcornHub.Web.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using TMDbLib.Client;

// aws secret 
// cache backed / frontend 
// try policy 
// rate limiting 
// export logs to sentry 
// real time message SSE 
// units and integration test
// use option pattern to avoid null reference 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection(nameof(JwtSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<ConnectionStringsSettings>()
    .Bind(builder.Configuration.GetSection(nameof(ConnectionStringsSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<CorsSettings>()
    .Bind(builder.Configuration.GetSection(nameof(CorsSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<TMDbClientSettings>()
    .Bind(builder.Configuration.GetSection(nameof(TMDbClientSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
using var serviceProvider = builder.Services.BuildServiceProvider();
var configService = serviceProvider.GetRequiredService<IConfigurationService>();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override(LoggingConstants.MicrosoftLogLevel, LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File(
        new CompactJsonFormatter(),
        path: LoggingConstants.LogFilePath,
        rollingInterval: RollingInterval.Day,
        shared: true
    ))
    .CreateLogger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(configService.CorsSettings.Policy, policy =>
    {
        policy.WithOrigins(configService.CorsSettings.AllowedOrigins)
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddAuthenticationJwtBearer(options =>
    {
        options.SigningKey = configService.JwtSettings.SigningKey;
    })
    .AddAuthorization()
    .AddFastEndpoints()
    .SwaggerDocument(options => options.AutoTagPathSegmentIndex = SwaggerEndpointTagging.AutoTagPathSegmentIndex );

builder.Services.AddTransient<GlobalErrorHandlingMiddleware>();
builder.Services.AddTransient<CookieToBearerInjectionMiddleware>();

builder.Services.AddDbContext<PopcornHubDbContext>(options =>
{
    options.UseNpgsql(configService.ConnectionStringsSettings.PostgresConnectionString);
});

builder.Services.AddScoped<IEfCoreRepository, EfCoreRepository>();
builder.Services.AddScoped<IReadOnlyEfCoreRepository, ReadOnlyEfCoreRepository>();
builder.Services.AddScoped<IRepositoryProvider, RepositoryProvider>();

builder.Services.AddScoped<IMovieExistenceChecker, TmdbMovieExistenceChecker>();

builder.Services.AddSingleton(new TMDbClient(configService.TMDbClientSettings.ApiKey));
builder.Services.AddSingleton<IMovieApiClient, MovieApiClient>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticationCookieHelper, AuthenticationCookieHelper>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var app = builder.Build();

app.UseCors(configService.CorsSettings.Policy);

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<CookieToBearerInjectionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();
