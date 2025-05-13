using Microsoft.AspNetCore.Authorization;
using PopcornHub.Shared.Constants;

namespace PopcornHub.Web.Middlewares;

public class CookieToBearerInjectionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var hasAllowAnonymous = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null;

        if (!hasAllowAnonymous && context.Request.Cookies.TryGetValue(JwtCookieConstants.AccessToken, out var token))
        {
            context.Request.Headers.Authorization = $"{JwtCookieConstants.BearerPrefix} {token}";
        }

        await next(context);
    }
}
