using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow
{
    internal class HybridFlow : Flow
    {
        public HybridFlow(OAuthContext context) : base(context) { }

        public override Task AuthenticatesEndUser()
        {
            throw new NotImplementedException();
        }
    }
}
