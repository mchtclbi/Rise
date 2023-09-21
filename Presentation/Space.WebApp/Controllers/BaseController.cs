using Microsoft.AspNetCore.Mvc;

namespace Space.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public void SetCookie(string key, string value, int? expireTime) => Response.Cookies.Append(key, value, new CookieOptions()
        {
            HttpOnly = true,
            Expires = expireTime.HasValue
                ? DateTime.Now.AddMinutes(expireTime.Value)
                : DateTime.Now.AddMinutes(30)
        });

        public string GetCookie(string key) => Request.Cookies[key];

        public void RemoveCookie(string key) => Response.Cookies.Delete(key);
    }
}