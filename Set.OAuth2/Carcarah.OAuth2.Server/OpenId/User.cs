using System;
using System.Linq.Expressions;

namespace Carcarah.OAuth2.Server.OpenId
{
    public class User
    {
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static Func<User, bool> Find(string username, string password) =>
            x => x.Username.Equals(username) && x.Password.Equals(password);
    }
}
