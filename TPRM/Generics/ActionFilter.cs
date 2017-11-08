using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TPRM.Contexts;
using TPRM.Models;

namespace TPRM.Generics
{
    public class ActionFilter : ActionFilterAttribute
    {
        private UsuarioCtx ctx = new UsuarioCtx();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var _Controlador = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var _Action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            var token = HttpContext.Current.Request.Cookies["token"];
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var obj = HttpContext.Current.Request.Cookies["token"] != null ?
                      json_serializer.Deserialize<Usuario>(ctx.DecryptText(HttpContext.Current.Request.Cookies["token"].Value)) :
                      null;
            if (_Controlador == "Empresa" && _Action == "Index" && (obj.perfil != null && obj.perfil.adm != "s" ))
                filterContext.Result = new RedirectResult("~/Home/Index");
            if (_Controlador == "Servico" && _Action == "Index" && (obj.perfil != null && obj.perfil.adm != "s"))
                filterContext.Result = new RedirectResult("~/Home/Index");
            if (_Controlador == "Usuario" && _Action == "Index" && (obj.perfil != null && obj.perfil.adm != "s"))
                filterContext.Result = new RedirectResult("~/Home/Index");
            if (_Controlador == "Transacao" && _Action == "Faturamento" &&(obj.perfil != null && obj.perfil.adm != "s"))
                filterContext.Result = new RedirectResult("~/Home/Index");
            if (token == null && _Controlador != "Login" && _Action =="Index")
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
            
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _Controlador = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var _Action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            var token = HttpContext.Current.Request.Cookies["token"];
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var obj = HttpContext.Current.Request.Cookies["token"] != null ?
                      json_serializer.Deserialize<Usuario>(ctx.DecryptText(HttpContext.Current.Request.Cookies["token"].Value)) :
                      null;
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (_Controlador == "Empresa" && (obj.perfil != null && obj.perfil.adm != "s"))
                    return;
                if (_Controlador == "Servico" && (obj.perfil != null && obj.perfil.adm != "s"))
                    return;
                if (_Controlador == "Usuario" && (obj.perfil != null && obj.perfil.adm != "s"))
                    return;
            }
            
        }

    }
}