using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["token"] == null)
                return Redirect("Login/Index");
            return View();
        }
        
    }
}