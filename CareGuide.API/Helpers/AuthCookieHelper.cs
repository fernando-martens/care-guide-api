namespace CareGuide.API.Helpers
{
    public class AuthCookieHelper
    {
        private const string CookieName = "refreshToken";

        public static void AppendRefreshToken(HttpResponse response, string refreshToken, int days = 1)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(days)
            };

            response.Cookies.Append(CookieName, refreshToken, cookieOptions);
        }

        public static void RemoveRefreshToken(HttpResponse response)
        {
            response.Cookies.Delete(CookieName);
        }
    }
}
