using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Helpers
{
    public static class CoockiesHelper
    {
        public static void SetCockie(HttpContext context, string name, string value)
        {
            if (context != null)
            {
                if (context.Request.Cookies.ContainsKey(name))
                {
                    context.Response.Cookies.Delete(name);
                }
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                context.Response.Cookies.Append(name, value, cookieOptions);
            }
        }
        public static string GetCockie(HttpContext context, string name)
        {
            string cookie = string.Empty;
            if (context != null && context.Request.Cookies.ContainsKey(name))
            {
                context.Request.Cookies.TryGetValue(name, out cookie);

            }
            return cookie;
        }
        public static void DeleteCockie(HttpContext context, string name)
        {
            string cookie = string.Empty;
            if (context != null && context.Request.Cookies.ContainsKey(name))
            {
                context.Response.Cookies.Delete(name);

            }
        }
    }
}
