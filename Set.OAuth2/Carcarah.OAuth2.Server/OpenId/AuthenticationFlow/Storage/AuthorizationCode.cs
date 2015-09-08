using Carcarah.OAuth2.Server.Helpers;
using System;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage
{
    public class AuthorizationCode
    {
        public DateTimeOffset CreationTime { get; set; }
        public Client Client { get; set; }
        public bool IsOpenId { get; set; }
        public string RedirectUri { get; set; }
        public string Nonce { get; set; }
        public string SubjectId { get; set; }

        public AuthorizationCode()
        {
            CreationTime = DateTimeOffsetHelper.UtcNow;
        }

        public string ClientId
        {
            get { return Client.ClientId; }
        }
    }
}