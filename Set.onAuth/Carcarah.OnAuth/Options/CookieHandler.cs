using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Options
{
    internal static class CookieHandler
    {
        public static string TOKEN_KEY_PREFIX = "set_onauth";

        public static void RegisterToken(this IOwinContext context, string token) => 
            context.Response.Cookies.Append(TOKEN_KEY_PREFIX, token);

        public static void RegisterAuthorizationCodeToken(this IOwinContext context, string token) =>
            context.Response.Cookies.Append($"{TOKEN_KEY_PREFIX}_authorization_code", token);

        public static void DeleteToken(this IOwinContext context) =>
            context.Response.Cookies.Delete(TOKEN_KEY_PREFIX);
    }
}
