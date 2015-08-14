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

        public AuthenticationRequestBody(IOwinContext context)
        {
            this.context = context;
        }

        public async Task<string> FindUserName()
        {
            if (context.Request.Body == null)
                return null;

            var form = await context.Request.ReadFormAsync();
            return form["username"];
        }

        public async Task<string> FindPassword()
        {
            if (context.Request.Body == null)
                return null;

            var form = await context.Request.ReadFormAsync();
            return form["password"];
        }
    }
}
