using Carcarah.OAuth2.Server.OpenId;
using System.Collections.Generic;

namespace Set.onAuth.Server.App_Start
{
    public class Users
    {
        public static List<User> Get()
        {
            return new List<User>
            {
                new User
                {
                    Subject = "2",
                    Username = "yan",
                    Password = "master1"
                }
            };
        }
    }
}