using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.Configuration
{
    public abstract class AuthorizationProvider
    {
        public abstract Task<bool> GrantResourceOwnerCredentials(OAuthContext context);
    }
}
