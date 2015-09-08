using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.Helpers
{
    internal static class CookiesHelper
    {
        public static string TOKEN_ID = "set.tid";
        public static string AUTHORIZATION_CODE = "set.atc";

        public static void Set(this IOwinContext context, string key, string token) =>
            context.Response.Cookies.Append(key, token);

        public static string Get(this IOwinContext context, string key) =>
            context.Request.Cookies[key];

        public static void Remove(this IOwinContext context, string key) =>
            context.Response.Cookies.Delete(key);

        public static void DeleteTokens(this IOwinContext context)
        {
            Remove(context, TOKEN_ID);
            Remove(context, AUTHORIZATION_CODE);
        }
    }
}
