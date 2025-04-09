using Microsoft.AspNetCore.Http;

namespace TicketToCode.Client.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetRole()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["auth"];
            if (cookie == null) return null;

            var parts = cookie.Split(':');
            return parts.Length == 2 ? parts[1] : null;
        }

        public string? GetUsername()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["auth"];
            if (cookie == null) return null;

            var parts = cookie.Split(':');
            return parts.Length == 2 ? parts[0] : null;
        }
    }
}
