using System;

namespace Carcarah.OnAuth.Server.OpenId.Request
{
    public class AuthenticationRequestException : Exception
    {
        public string Description { get; private set; }

        public AuthenticationRequestException(string error, string description) :base(error)
        {
            this.Description = description;
        }
    }
}
