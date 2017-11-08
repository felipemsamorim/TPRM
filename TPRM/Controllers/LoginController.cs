using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPRM.Contexts;
using TPRM.Models;
using Newtonsoft.Json;

namespace TPRM.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private UsuarioCtx ctx = new UsuarioCtx();
        public ActionResult Index()
        {
            if (Request.Cookies["token"] != null)
                return Redirect("~/Home/");
            return View();
        }

        public bool Logar (Usuario user)
        {
            Usuario u = ctx.login(user.login, user.senha);
            if ( u != null)
            {
                Response.Cookies["usuario"].Value = u.login;
                Response.Cookies["token"].Value = ctx.EncryptText(JsonConvert.SerializeObject(u));
                return true;
            }
            return false;

        }
        public ActionResult Logout()
        {
            Response.Cookies["usuario"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("~/Login/");
        }
    }
}