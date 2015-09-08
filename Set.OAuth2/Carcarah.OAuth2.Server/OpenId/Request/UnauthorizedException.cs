using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    internal class UnauthorizedException : Exception
    {
        public string prompt { get; private set; }
        public string error { get; private set; }
        public string error_description { get; private set; }

        public UnauthorizedException(string prompt = "none")
        {
            this.prompt = prompt;
            this.error = "login_required";
            this.error_description = "The Authorization Server requires End-User authentication";
        }

        public UnauthorizedException(string erro, string description)
        {
            this.prompt = "none";
            this.error = erro;
            this.error_description = description;
        }
    }
}
