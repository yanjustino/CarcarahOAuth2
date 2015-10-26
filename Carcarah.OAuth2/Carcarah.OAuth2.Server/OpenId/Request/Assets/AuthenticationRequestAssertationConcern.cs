namespace Carcarah.OAuth2.Server.OpenId.Request
{
    public class AuthenticationRequestAssertationConcern
    {
        public static void IsNotNull(object key, string error, string message)
        {
            if (key == null)
                throw new AuthenticationRequestException(error, message);
        }

        public static void Contains(string key, string value, string error, string message)
        {
            if (!key.Contains(value))
                throw new AuthenticationRequestException("invalid_request", message);
        }

        public static void IsTrue(bool value, string error, string message)
        {
            if (value == false)
                throw new AuthenticationRequestException("invalid_request", message);
        }
    }
}
