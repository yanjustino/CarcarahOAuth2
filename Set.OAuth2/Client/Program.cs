using Carcarah.OAuth2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Start().Result;
            Console.WriteLine(t);
            Console.Read();
        }

        private static async Task<bool> Start()
        {
            try
            {
                OAuth2Client client = new OAuth2Client("b00d1174dbc47e846795dbefcf99d825945499fe52a9eabded8781232300d69b");

                var result = await client.AuthorizationGrantAsync
                    (
                        new AuthorizationGrantRequest
                        {
                            //Uri = "http://onauth2.azurewebsites.net/",
                            Uri = "http://localhost:53586/",
                            ClientId = "635678CD-FB15-4645-A044-6FEB69E70DC8",
                            RedirectUri = "http://localhost/client",
                            Username = "yan",
                            Password = "master1"
                        }
                    );

                Console.WriteLine(result.id_token);

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            Console.Read();
        }
    }
}
