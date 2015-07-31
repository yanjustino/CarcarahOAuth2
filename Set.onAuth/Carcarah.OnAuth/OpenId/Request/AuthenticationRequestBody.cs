using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    internal class AuthenticationRequestBody
    {
        private IOwinContext _context;

        public AuthenticationRequestBody(IOwinContext context)
        {
            _context = context;
        }

        public async Task<string> FindUserName()
        {
            var r = await _context.Request.ReadFormAsync();
            return r["username"];
        }

        public async Task<string> FindPassword()
        {
            var r = await _context.Request.ReadFormAsync();
            return r["password"];
        }
    }
}
