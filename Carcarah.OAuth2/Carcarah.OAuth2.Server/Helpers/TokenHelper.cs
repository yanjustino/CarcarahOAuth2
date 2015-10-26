using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.Helpers
{
    public static class TokenHelper
    {
        public static string ToSha256String(this string value)
        {
            var sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                var encript = Encoding.UTF8;
                var results = hash.ComputeHash(encript.GetBytes(value));

                foreach (Byte b in results)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string ToBase64String(this string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }
    }
}
