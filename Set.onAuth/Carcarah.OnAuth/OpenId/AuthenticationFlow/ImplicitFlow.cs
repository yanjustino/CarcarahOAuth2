using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcarah.OnAuth.OpenId.Request;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public class ImplicitFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public ImplicitFlow(CarcarahOnAuthContext context) : base(context) { }

        protected override string SuccessfulAuthenticationResponse()
        {
            throw new NotImplementedException();
        }
    }
}
