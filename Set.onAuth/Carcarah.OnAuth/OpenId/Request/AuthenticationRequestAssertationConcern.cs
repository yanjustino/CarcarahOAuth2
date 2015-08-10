using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    public class RequestAssertationConcern
    {
        public static void IsNotNull(string key, string message)
        {
            if (key == null)
                throw new AuthenticationRequestException("invalid_request", message);
        }

        public static void Contains(string key, string value, string message)
        {
            if (!key.Contains(value))
                throw new AuthenticationRequestException("invalid_request", message);
        }

    }
}
