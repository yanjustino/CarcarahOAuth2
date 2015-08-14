using Carcarah.OnAuth.OpenId;
using Microsoft.Owin;
using System.Linq;
using System.Collections.Generic;

namespace Carcarah.OnAuth.Options
{
    public class OnAuthOptions
    {
        public PathString AuthorizationUri { get; set; }
        public AuthorizationProvider AuthorizationProvider { get; set; }
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }

        internal PathString AuthorizationEndpoint =>
            new PathString("/authorization");

        internal PathString EndSessionEndPoint => 
            new PathString("/endsession");

        internal PathString TokenEndPoint =>
            new PathString("/token");
    }
}
