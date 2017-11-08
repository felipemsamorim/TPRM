using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPRM.Contexts;
using TPRM.Models;

namespace TPRM.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        private UsuarioCtx ctx = new UsuarioCtx();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public JsonResult getUsuarios()
        {
            return Json(ctx.getUsuarios(), JsonRequestBehavior.AllowGet);
        }
        public Usuario getUsuario(int id)
        {
            return ctx.getUsuario(id);
        }
        public void createUser(Usuario user)
        {
            if(user.perfil == null)
            {
                ctx.createUsuario(user.login, user.senha,user.empresa);
            }
            else
            {
                ctx.createUsuario(user.login, user.senha,user.perfil,user.empresa);
            }
            
        }
        public void deleteUsuario(int id)
        {
            ctx.deleteUsuario(id);
        }
        public void updateUsuario(Usuario user)
        {
            ctx.updateUsuario(user);
        }

        public JsonResult getPerfilAll()
        {
            return Json(ctx.getPerfilAll(), JsonRequestBehavior.AllowGet);
        }
        public Perfil getPerfil(int id)
        {
            return ctx.getPerfil(id);
        }
        public void createPerfil(Perfil perfil)
        {
            ctx.createPerfil(perfil);
        }
        public void updatePerfil(Perfil p)
        {
            ctx.updatePerfil(p);
        }
    }
}