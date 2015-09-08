using System;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow.Flows
{
    public class ImplicitFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public ImplicitFlow(CarcarahOAuthContext context) : base(context) { }

        public Task AuthorizeEndUser()
        {
            throw new NotImplementedException();
        }

        public Task TokenRequestValidation()
        {
            throw new NotImplementedException();
        }
    }
}
