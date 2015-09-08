using Carcarah.OnAuth.Server.OpenId;
using Microsoft.Owin;
using System.Linq;
using System.Collections.Generic;

namespace Carcarah.OnAuth.Server.Configuration
{
    public class OAuthOptions
    {
        public PathString AuthorizationUri { get; set; }
        public AuthorizationProvider AuthorizationProvider { get; set; }
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }
        public string SecretKey { get; set; }

        internal Client ClientById(string id) =>
            Clients.FirstOrDefault(x => x.ClientId == id);
    }
}
