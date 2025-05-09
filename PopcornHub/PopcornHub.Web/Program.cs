using System.Text.Json;
using Duende.IdentityServer.Models;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PopcornHub.Application.Services;
using PopcornHub.Domain.DTOs.Movies;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.IRepository;
using PopcornHub.Domain.IServices;
using PopcornHub.Infrastructure.Context;
using PopcornHub.Infrastructure.Repository;
using PopcornHub.Web.Middlewares;
using PopcornHub.Web.Validators;
using Serilog;
using TMDbLib.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging
    .ClearProviders()
    .AddJsonConsole(x =>
    {
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .AuditTo.File("audit-logs.json")
    .CreateLogger();


builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<PopcornHubDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.GetClients()) // Configurezi aplicațiile client
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddAspNetIdentity<User>() // Leagă IdentityServer cu UserManager pentru a gestiona utilizatorii
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Authority"];
        options.Audience = "api1";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddScoped<ITokenService, TokenService>(); 


builder.Services.AddSingleton(new TMDbClient("ff4cdfd3b8ac48623f5843233ea7e050"));
builder.Services.AddTransient<GlobalErrorHandlingMiddleware>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddFastEndpoints()
    .SwaggerDocument(options => options.AutoTagPathSegmentIndex = 2 );
builder.Services.AddScoped<IValidator<SearchMoviesRequest>, SearchMoviesRequestValidator>();

builder.Services.AddDbContext<PopcornHubDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseFastEndpoints().UseSwaggerGen();
app.UseHttpsRedirection();
app.UseIdentityServer(); 
app.Run();

// check auth 
// create struct react 
// pagination
// secret keys amd key value pair AWS
// real time message SSR Redis or AWS 
// caching 
// validation
// aggregates
// rate limiting
// try policy use polly 
// improve logging and export to sentry or seq
// create consts 
// refactor endpoints
// refactor program cs
// ----------------------------
// unit tests
// integration tests
// mocks for UI 
// documentation: flows, technologies, arhitecture   
return;

// Definirea clienților IdentityServer
public static class Config
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "google-client",
                ClientSecrets = { new Secret("google-client-secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://yourapp.com/auth/google/callback" },
                PostLogoutRedirectUris = { "https://yourapp.com/" },
                AllowedScopes = { "openid", "profile", "email", "api1" },
                AllowOfflineAccess = true,  // Permite refresh token
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api1", "API 1")
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}


