using System;
using System.Security.Cryptography;
using System.Text;

namespace Carcarah.OnAuth.Server.OpenId.Token
{
    public static class TokenHandler
    {
        public static string Sha256(this string value)
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

        public static double GenerateExp(int seconds)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Math.Round((DateTime.UtcNow.AddSeconds(seconds) - unixEpoch).TotalSeconds);
        }
    }
}
