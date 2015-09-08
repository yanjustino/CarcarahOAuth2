using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.Configuration
{
    public abstract class AuthorizationProvider
    {
        public abstract Task<bool> GrantResourceOwnerCredentials(CarcarahOAuthContext context);
    }
}
