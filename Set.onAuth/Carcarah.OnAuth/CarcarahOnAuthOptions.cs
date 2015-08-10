using Carcarah.OnAuth.Config;
using Microsoft.Owin;

namespace Carcarah.OnAuth
{
    public class CarcarahOnAuthOptions
    {
        public PathString AuthorizationEndpoint { get; set; }
        public CarcarahAuthorizationProvider AuthorizationProvider { get; set; }

        internal PathString EndSessionEndPoint => 
            new PathString("/endsession");

        internal PathString IdTokenEndPoint =>
            new PathString("/token");
    }
}
