using Microsoft.Owin;

namespace Carcarah.OnAuth.Server.OpenId.Request
{
    public class AuthenticationRequestQuery
    {
        private IOwinContext context;

        public string scope { get; }
        public string response_type { get; }
        public string client_id { get; }
        public string redirect_uri { get; }
        public string state { get; }
        public string prompt { get; }
        public string grant_type { get; }
        public string code { get; }

        public AuthenticationRequestQuery(IOwinContext context)
        {
            this.context = context;
            this.scope = context.Request.Query["scope"];
            this.response_type = context.Request.Query["response_type"];
            this.client_id = context.Request.Query["client_id"];
            this.redirect_uri = context.Request.Query["redirect_uri"];
            this.state = context.Request.Query["state"];
            this.prompt = context.Request.Query["prompt"];
            this.grant_type = context.Request.Query["grant_type"];
            this.code = context.Request.Query["code"];
        }

    }
}
