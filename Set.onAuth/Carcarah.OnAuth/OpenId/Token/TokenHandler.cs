using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Token
{
    public class TokenHandler
    {
        public static string Sha256(string value)
        {
            var sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                var encript = Encoding.UTF8;
                var results = hash.ComputeHash(encript.GetBytes(value));

                foreach (Byte b in results)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
