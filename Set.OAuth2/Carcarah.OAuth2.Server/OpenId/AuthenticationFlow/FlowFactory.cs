using Carcarah.OAuth2.Server.Configuration;
using Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage;
using Carcarah.OAuth2.Server.OpenId.Request;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow
{
    public class FlowFactory
    {
        public static Flow Get(
            IOwinContext owinContext, 
            OAuthOptions options, 
            AuthorizationCodeStorage authCodeStorage,
            RefreshTokenStorage refreshTokenStorage
            )
        {
            var request = new AuthenticationRequest(owinContext);
            var context = new OAuthContext(options, request, owinContext);

            switch (context.AuthenticationRequest.response_type?.Trim())
            {
                case "code": return new AuthorizationCodeFlow(context, authCodeStorage, refreshTokenStorage);
                case "id_token": return new ImplicitFlow(context);
                case "id_token token": return new ImplicitFlow(context);
                case "code id_token": return new HybridFlow(context);
                case "code token": return new HybridFlow(context);
                case "code id_token token": return new HybridFlow(context);
                default: return null;
            }
        }

        public static TokenEndPoint GetTokenEndProint(
            IOwinContext owinContext, 
            OAuthOptions options, 
            AuthorizationCodeStorage storage,
            RefreshTokenStorage refreshTokenStorage
            )
        {
            var request = new TokenRequest(owinContext);
            var context = new OAuthContext(options, request, owinContext);

            return new TokenEndPoint(context, storage, refreshTokenStorage);
        }
    }
}
