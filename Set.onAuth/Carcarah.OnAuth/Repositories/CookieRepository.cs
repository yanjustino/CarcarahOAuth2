using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Repositories
{
    internal static class CookieRepository
    {
        private static string KEY_TOKEN_ID = "STI";
        private static string KEY_AUTHORIZATION_CODE = "STAC";

        public static void RegisterToken(this IOwinContext context, string token) =>
            context.Response.Cookies.Append(KEY_TOKEN_ID, token);

        public static void DeleteToken(this IOwinContext context) =>
            context.Response.Cookies.Delete(KEY_TOKEN_ID);

        public static void RegisterAuthorizationCodeToken(this IOwinContext context, string token) =>
            context.Response.Cookies.Append(KEY_AUTHORIZATION_CODE, token);

        public static string GetAuthorizationCodeToken(this IOwinContext context) =>
            context.Request.Cookies[KEY_AUTHORIZATION_CODE];
    }
}
