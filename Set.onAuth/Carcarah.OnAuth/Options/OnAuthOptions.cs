using Carcarah.OnAuth.OpenId;
using Microsoft.Owin;
using System.Linq;
using System.Collections.Generic;

namespace Carcarah.OnAuth.Options
{
    public class OnAuthOptions
    {
        public PathString AuthorizationEndpoint { get; set; }
        public AuthorizationProvider AuthorizationProvider { get; set; }
        public List<Client> Clients { get; set; }

        internal PathString EndSessionEndPoint => 
            new PathString("/endsession");

        internal PathString IdTokenEndPoint =>
            new PathString("/token");
    }
}
