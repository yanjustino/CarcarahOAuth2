using Carcarah.OnAuth.OpenId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Set.onAuth.Server.App_Start
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "UVT",
                    ClientId = "635678CD-FB15-4645-A044-6FEB69E70DC8",
                    ClientSecrets = new List<string>
                    {
                        "b00d1174dbc47e846795dbefcf99d825945499fe52a9eabded8781232300d69b"
                    },
                }
            };
        }
    }
}