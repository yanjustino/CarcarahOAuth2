using Carcarah.OnAuth.Config;
using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    public class AuthenticationRequest
    {
        internal IOwinContext Context { get; }
        internal PathString EndPoint => Context.Request.Path;
        internal RequestCookieCollection Cookies => Context.Request.Cookies;

        public AuthenticationRequestQuery Query { get; }
        public AuthenticationRequestBody Body { get; }

        public AuthenticationRequest(IOwinContext context)
        {
            this.Context = context;
            this.Query = new AuthenticationRequestQuery(context);
            this.Body = new AuthenticationRequestBody(context);

            ValidateAuthenticationRequest();
        }

        public void ValidateAuthenticationRequest()
        {
            RequestAssertationConcern.IsNotNull(Query.scope, "scope is required");
            RequestAssertationConcern.IsNotNull(Query.response_type, "response_type is required");
            RequestAssertationConcern.IsNotNull(Query.client_id, "client_id is required");
            RequestAssertationConcern.IsNotNull(Query.redirect_uri, "redirect_uri is required");
            RequestAssertationConcern.IsNotNull(Query.state, "state is required");

            RequestAssertationConcern.Contains(Query.scope, "openid", "'openid' value no present");
        }

        public bool TokenRegistered() =>
            Cookies.Any(x => x.Key == CarcarahCookieHandler.TOKEN_KEY);
    }
}
