using Microsoft.Owin;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    internal static class RequestRoutes
    {
        internal static PathString EndSessionEndPoint => new PathString("/endsession");
        internal static PathString TokenEndPoint => new PathString("/token");
    }
}
