namespace Carcarah.OnAuth.Server.OpenId.Request
{
    public class RequestAssertationConcern
    {
        public static void IsNotNull(object key, string message)
        {
            if (key == null)
                throw new AuthenticationRequestException("invalid_request", message);
        }

        public static void Contains(string key, string value, string message)
        {
            if (!key.Contains(value))
                throw new AuthenticationRequestException("invalid_request", message);
        }

        public static void IsTrue(bool value, string message)
        {
            if (value == false)
                throw new AuthenticationRequestException("invalid_request", message);
        }
    }
}
