using Microsoft.AspNetCore.Http;
using PopcornHub.Shared.Constants;

namespace PopcornHub.Shared.Utils;

public class AuthenticationCookieHelper : IAuthenticationCookieHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationCookieHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetCookie(string token)
    {
        var response = _httpContextAccessor.HttpContext?.Response;
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, 
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        response?.Cookies.Append(JwtCookieConstants.AccessToken, token, cookieOptions);
    }
}
