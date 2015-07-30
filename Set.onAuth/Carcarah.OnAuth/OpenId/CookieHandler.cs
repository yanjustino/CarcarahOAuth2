using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    internal static class CookieHandler
    {
        private static string TOKEN_KEY = "set.rn.gov.br.onauth";

        public static bool TokenRegistered(this IOwinContext context) => 
            context.Request.Cookies.Any(x => x.Key == TOKEN_KEY);

        public static void RegisterToken(this IOwinContext context, string token) => 
            context.Response.Cookies.Append(TOKEN_KEY, token);
    }
}
