using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Config
{
    internal static class CarcarahCookieHandler
    {
        public static string TOKEN_KEY = "set.rn.gov.br.onauth";

        public static void RegisterToken(this IOwinContext context, string token) => 
            context.Response.Cookies.Append(TOKEN_KEY, token);

        public static void DeleteToken(this IOwinContext context) =>
            context.Response.Cookies.Delete(TOKEN_KEY);
    }
}
