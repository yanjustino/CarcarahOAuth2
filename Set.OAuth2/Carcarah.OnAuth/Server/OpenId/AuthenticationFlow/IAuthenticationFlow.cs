using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow
{
    public interface IAuthenticationFlow
    {
        Task AuthorizeEndUser();
        Task TokenRequestValidation();
    }
}
