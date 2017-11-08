using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPRM.Contexts;
using TPRM.Models;

namespace TPRM.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        private EmpresaCtx ctx = new EmpresaCtx();
        public ActionResult Index()
        {
            return View();
        }

        public Empresa getEmpresa(int id)
        {
            return ctx.getEmpresa(id);
        }
        public JsonResult getAll()
        {
            return Json(ctx.getAll(), JsonRequestBehavior.AllowGet);
        }
        public void createEmpresa(Empresa e)
        {
            ctx.createEmpresa(e);
        }
        public void updateEmpresa(Empresa e)
        {
            ctx.updateEmpresa(e);
        }
        public void deleteEmpresa(int id)
        {
            ctx.deleteEmpresa(id);
        }
    }
}