using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Set.onAuth.Server.Controllers
{
    public class OnAuthController : Controller
    {
        // GET: OnAuth
        public ActionResult Authorize()
        {
            return View();
        }
    }
}