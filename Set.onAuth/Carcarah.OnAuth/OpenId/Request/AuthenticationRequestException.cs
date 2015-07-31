using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
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
