using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;
using Carcarah.OnAuth.OpenId.Request;

namespace Carcarah.OnAuth.Config
{
    public abstract class CarcarahAuthorizationProvider
    {
        public abstract Task<bool> GrantResourceOwnerCredentials(CarcarahOnAuthContext context);

        internal virtual bool HasValidateCookieAuthentication(CarcarahOnAuthContext context)
        {
            return context.Request.TokenRegistered();
        }
    }
}
