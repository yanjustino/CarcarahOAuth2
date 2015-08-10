namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public class AuthenticationFlowFactory
    {
        public static IAuthenticationFlow New(CarcarahOnAuthContext context)
        {
            switch (context.Request.Query.response_type.Trim())
            {
                case "code": return new AuthorizationCodeFlow(context);
                case "id_token": return new ImplicitFlow(context);
                case "id_token token": return new ImplicitFlow(context);
                case "code id_token": return new HybridFlow(context);
                case "code token": return new HybridFlow(context);
                case "code id_token token": return new HybridFlow(context);
                default: return null;
            }
        }
    }
}
