using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;
using Carcarah.OnAuth.OpenId.Request;

namespace Carcarah.OnAuth.Options
{
    public abstract class AuthorizationProvider
    {
        public abstract Task<bool> GrantResourceOwnerCredentials(CarcarahOnAuthContext context);
    }
}
