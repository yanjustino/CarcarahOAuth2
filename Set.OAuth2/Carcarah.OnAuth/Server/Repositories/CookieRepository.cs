using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.Repositories
{
    internal static class CookieRepository
    {
        public static string TOKEN_ID = "SET_STI";
        public static string AUTHORIZATION_CODE = "SET_STAC";
        public static string CLIENT_SECRET_JWT = "SET_CSJ";

        public static void Set(this IOwinContext context, string key, string token) =>
            context.Response.Cookies.Append(key, token);

        public static string Get(this IOwinContext context, string key) =>
            context.Request.Cookies[key];

        public static void Remove(this IOwinContext context, string key) =>
            context.Response.Cookies.Delete(key);

        public static void ClearAllTokens(this IOwinContext context)
        {
            Remove(context, TOKEN_ID);
            Remove(context, AUTHORIZATION_CODE);
            Remove(context, CLIENT_SECRET_JWT);
        }
    }
}
