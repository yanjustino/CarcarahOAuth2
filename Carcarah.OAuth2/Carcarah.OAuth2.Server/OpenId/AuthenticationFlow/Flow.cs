using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow
{
    public abstract class Flow
    {
        public OAuthContext Context { get; set; }

        public Flow(OAuthContext context)
        {
            Context = context;
        }

        public abstract Task AuthenticatesEndUser();
    }
}
