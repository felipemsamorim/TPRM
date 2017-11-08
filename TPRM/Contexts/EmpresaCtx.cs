using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPRM.DA;
using TPRM.Models;

namespace TPRM.Contexts
{
    public class EmpresaCtx
    {
        public Empresa getEmpresa(int id)
        {
            return new TPRMContext().Empresa.Where(x => x.id == id).SingleOrDefault();
        }
        public List<Empresa> getAll()
        {
            return new TPRMContext().Empresa.ToList();
        }
        public void createEmpresa(Empresa e)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Empresa.Add(e);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void updateEmpresa(Empresa e)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Empresa.Where(x => x.id == e.id).SingleOrDefault().cnpj = e.cnpj;
                    ctx.Empresa.Where(x => x.id == e.id).SingleOrDefault().nomefantasia = e.nomefantasia;
                    ctx.Empresa.Where(x => x.id == e.id).SingleOrDefault().razaosocial = e.razaosocial;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void deleteEmpresa(int id)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Empresa.Remove(ctx.Empresa.Where(x => x.id == id).SingleOrDefault());
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
    }
}