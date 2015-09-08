using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Client
{
    public class AuthorizationGrantRequest
    {
        public string Uri { get; set; }
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
    }
}
