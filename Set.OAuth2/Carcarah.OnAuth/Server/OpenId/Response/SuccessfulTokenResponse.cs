namespace Carcarah.OnAuth.Server.OpenId.Response
{
    public class SuccessfulTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string id_token { get; set; }

        public string GenerateJson()
        {
            return
            "{" +
            $"    \"access_token\": \"{access_token}\"," +
            $"    \"token_type\": \"{token_type}\"," +
            $"    \"refresh_token\": \"{refresh_token}\"," +
            $"    \"expires_in\": \"{expires_in}\"," +
            $"    \"id_token\": \"{id_token}\"" +
            "}";
        }
    }
}
