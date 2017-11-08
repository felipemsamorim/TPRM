using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPRM.Contexts;
using TPRM.Models;

namespace TPRM.Controllers
{
    public class ServicoController : Controller
    {
        // GET: Servico
        private ServicoCtx ctx = new ServicoCtx();
        public ActionResult Index()
        {
            return View();
        }

        public Servico getServico(int id)
        {
            return ctx.getServico(id);
        }
        public JsonResult getAll()
        {
            return Json(ctx.getAll(), JsonRequestBehavior.AllowGet);
        }
        public void createServico(Servico e)
        {
            ctx.createServico(e);
        }
        public void updateServico(Servico e)
        {
            ctx.updateServico(e);
        }
        public void deleteServico(int id)
        {
            ctx.deleteServico(id);
        }
    }
}