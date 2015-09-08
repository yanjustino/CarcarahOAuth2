using Carcarah.OnAuth.Server.OpenId.Token;
using System;
using System.Threading.Tasks;
using Carcarah.OnAuth.Server.OpenId.Response;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow.Flows
{
    public class AuthorizationCodeFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public AuthorizationCodeFlow(CarcarahOAuthContext context) : base(context) { }

        public async Task AuthorizeEndUser()
        {
            var IsAuthorized = await Context.FindUserInMemory() ||
                               await Context.GrantResourceOwnerCredentials();

            if (!IsAuthorized)
                OwinContext.Unauthorized(Context.Options);
            else
            {
                if (Context.Request.IsPost)
                {
                    var code = Guid.NewGuid().ToString().Sha256();
                    ClientSecretJwtToken.Register(Context);
                    AuthorizationCodeToken.Register(Context, code);

                    var response = new SuccessfulAuthenticationResponse
                    {
                        code = code,
                        state = Context.State
                    };

                    var redirect_uri = $"{Context.Request.Query.redirect_uri}?{response.ToString()}";

                    OwinContext.Authorized(redirect_uri);
                }
                else
                {
                    OwinContext.SuccessfulToken(TokenId.Register(Context, Context.Client));
                }
            }
        }

        public Task TokenRequestValidation()
        {
            var client = ClientSecretJwtToken.Get(Context);

            if (!AuthorizationCodeToken.Validate(Context, client))
                OwinContext.TokenErrorResponse();
            else
            {
                var response = TokenId.Register(Context, client);
                OwinContext.SuccessfulToken(response);
            }

            return Task.FromResult<int>(0);
        }
    }
}
