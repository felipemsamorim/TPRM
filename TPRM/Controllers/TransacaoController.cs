using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPRM.Contexts;
using TPRM.Models;
using System.Web.Script.Serialization;

namespace TPRM.Controllers
{
    public class TransacaoController : Controller
    {
        // GET: Transacao
        private TransacaoCtx ctx = new TransacaoCtx();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faturamento()
        {
            return View();
        }

        public JsonResult getTransacoes()
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            UsuarioCtx Uctx = new UsuarioCtx();
            var objToken = json_serializer
                                        .Deserialize<Usuario>(Uctx.DecryptText(Request.Cookies["token"].Value));
            int idEmpresa = Request.Cookies["token"] == null || 
                            objToken.empresa == null ? 0 
                            : objToken.empresa.id;
            if (idEmpresa != 0 && objToken.perfil.analista != "s" && objToken.perfil.adm != "s")
            return Json(ctx.getTransacaoAll().Where(x => x.empresa_contratante.id == idEmpresa), JsonRequestBehavior.AllowGet);
            return Json(ctx.getTransacaoAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getTransacoesFaturamento()
        {
            return Json(ctx.getTransacoesFaturamento(), JsonRequestBehavior.AllowGet);
        }
        public void Aprovar(int id)
        {
            ctx.Aprovar(id);
        }
        public void Reprovar(int id)
        {
            ctx.Reprovar(id);

        }
        public void Pendente(int id)
        {
            ctx.Pendente(id);

        }
        public void Salvar(Transacao t)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            UsuarioCtx Uctx = new UsuarioCtx();
            t.empresa_contratante = new Empresa();
            t.empresa_contratante.id = json_serializer
                                        .Deserialize<Usuario>(Uctx.DecryptText(Request.Cookies["token"].Value))
                                        .empresa.id;
            ctx.Salvar(t);
        }
    }
}