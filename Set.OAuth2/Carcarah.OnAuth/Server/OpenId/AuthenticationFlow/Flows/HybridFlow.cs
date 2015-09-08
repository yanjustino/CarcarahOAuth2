using System;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow.Flows
{
    public class HybridFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public HybridFlow(CarcarahOAuthContext context) : base(context) { }

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
