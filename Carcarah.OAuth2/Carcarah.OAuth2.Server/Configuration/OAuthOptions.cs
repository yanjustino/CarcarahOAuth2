using Carcarah.OAuth2.Server.OpenId;
using Microsoft.Owin;
using System.Linq;
using System.Collections.Generic;

namespace Carcarah.OAuth2.Server.Configuration
{
    public class OAuthOptions
    {
        public AuthorizationProvider AuthorizationProvider { get; set; }
        public PathString AuthorizationUri { get; set; }
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }
    }
}
