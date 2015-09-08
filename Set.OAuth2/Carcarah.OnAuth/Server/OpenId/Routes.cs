using Microsoft.Owin;

namespace Carcarah.OnAuth.Server.OpenId
{
    internal static class Routes
    {
        internal static PathString AuthorizationEndpoint => new PathString("/authorization");
        internal static PathString EndSessionEndPoint => new PathString("/endsession");
        internal static PathString TokenEndPoint => new PathString("/token");
    }
}
