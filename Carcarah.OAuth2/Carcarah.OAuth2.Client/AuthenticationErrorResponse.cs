using System;

namespace Carcarah.OAuth2.Client
{
    [Serializable]
    public class AuthenticationErrorResponse
    {
        public string error { get; set; }
        public string state { get; set; }
        public string error_description { get; set; }
    }

}
