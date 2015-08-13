using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcarah.OnAuth.OpenId.Request;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow.Flows
{
    public class HybridFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public HybridFlow(CarcarahOnAuthContext context) : base(context) { }

        protected override string SuccessfulAuthenticationResponse()
        {
            throw new NotImplementedException();
        }
    }
}
