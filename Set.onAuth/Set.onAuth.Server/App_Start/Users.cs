using Carcarah.OnAuth.OpenId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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