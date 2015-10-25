﻿namespace Carcarah.OAuth2.Client
{
    public class SuccessfulTokenResponse
    {
        public string access_token { get; set; }
        public string token_type => "Bearer";
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string id_token { get; set; }
    }
}
