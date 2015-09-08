using Microsoft.Owin;

namespace Carcarah.OnAuth.Server.OpenId.Request
{
    public class AuthenticationRequest
    {
        internal IOwinContext Context { get; }
        internal PathString EndPoint => Context.Request.Path;
        internal RequestCookieCollection Cookies => Context.Request.Cookies;

        public AuthenticationRequestQuery Query { get; }
        public AuthenticationRequestBody Body { get; }
        public bool IsPost { get; }

        public AuthenticationRequest(IOwinContext context)
        {
            Context = context;
            Query = new AuthenticationRequestQuery(context);
            Body = new AuthenticationRequestBody(context);
            IsPost = context.Request.Method.Equals("POST");

            ValidateAuthenticationRequest();
        }

        public void ValidateAuthenticationRequest()
        {
            if (!Context.Request.Path.Equals(Routes.TokenEndPoint))
            {
                if (!Context.Request.QueryString.HasValue)
                    throw new AuthenticationRequestException(
                        "invalid_request_uri", 
                        "request uri is required");

                RequestAssertationConcern.IsNotNull(Query.scope, "scope is required");
                RequestAssertationConcern.IsNotNull(Query.response_type, "response_type is required");
                RequestAssertationConcern.IsNotNull(Query.client_id, "client_id is required");
                RequestAssertationConcern.IsNotNull(Query.redirect_uri, "redirect_uri is required");

                RequestAssertationConcern.Contains(Query.scope, "openid", "'openid' value no present");
            }
        }
    }
}
