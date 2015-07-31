using Carcarah.OnAuth.Config;
using Microsoft.Owin;

namespace Carcarah.OnAuth
{
    public class CarcarahOnAuthOptions
    {
        public PathString AuthorizationEndpoint { get; set; }
        public CarcarahAuthorizationProvider AuthorizationProvider { get; set; }
    }
}
