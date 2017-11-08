using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPRM.Contexts;
using TPRM.Models;

namespace TPRM.DA
{
    public class TPRMInitialize: System.Data.Entity.CreateDatabaseIfNotExists<TPRMContext>{
        private UsuarioCtx UCtx = new UsuarioCtx();
        protected override void Seed(TPRMContext ctx) {
            base.Seed(ctx);
            Usuario admin = new Usuario();
            admin.login = "admin";
            admin.senha = UCtx.EncryptText("fmsa");
            ctx.Usuario.Add(admin);
        }
    }
    
}