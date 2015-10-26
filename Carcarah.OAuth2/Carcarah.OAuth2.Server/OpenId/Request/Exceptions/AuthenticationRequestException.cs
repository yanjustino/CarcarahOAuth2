using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    class AuthenticationRequestException : Exception
    {
        public string error { get; private set; }
        public string error_description { get; private set; }

        public AuthenticationRequestException(string error, string description)
        {
            this.error = error;
            this.error_description = description;
        }
    }
}
