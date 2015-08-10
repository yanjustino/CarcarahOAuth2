using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    public class AuthenticationRequestBody
    {
        private IOwinContext context;

        public string UserName => FindUserName()?.Result;
        public string Password => FindPassword()?.Result;

        public AuthenticationRequestBody(IOwinContext context)
        {
            this.context = context;
        }

        private async Task<string> FindUserName()
        {
            if (context.Request.Body == null)
                return null;

            var form = await context.Request.ReadFormAsync();
            return form["username"];
        }

        private async Task<string> FindPassword()
        {
            if (context.Request.Body == null)
                return null;

            var form = await context.Request.ReadFormAsync();
            return form["password"];
        }
    }
}
