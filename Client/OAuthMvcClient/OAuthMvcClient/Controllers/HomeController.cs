using Carcarah.OnAuth.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthMvcClient.Controllers
{
    public class HomeController : Controller
    {
        OAuthClient oauth = new OAuthClient(
            new Uri("http://localhost:53586"),
            "635678CD-FB15-4645-A044-6FEB69E70DC8",
            "b00d1174dbc47e846795dbefcf99d825945499fe52a9eabded8781232300d69b");

        public ActionResult Index()
        {
            //var url = oauth.RequestClientCredentialsAsync("UVT", "http://localhost:64783/home/about");
            oauth.RequestResourceOwnerPasswordAsync("yan", "master1", "UVT", "http://localhost:64783/home/about");

            return Redirect("about");
        }

        public ActionResult About()
        {
            string HtmlResult = oauth.TokenRequest(Request, "http://localhost:64783/home/about");

            return View();
        }


        public ActionResult Contact()
        {

            return View();
        }
    }
}