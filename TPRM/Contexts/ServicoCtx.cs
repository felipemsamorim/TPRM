using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPRM.DA;
using TPRM.Models;

namespace TPRM.Contexts
{
    public class ServicoCtx
    {
        
        public List<Servico> getAll()
        {
            return new TPRMContext().Servico.ToList();
        }
        public Servico getServico(int id)
        {
            return new TPRMContext().Servico.Where(x => x.id == id).SingleOrDefault();
        }
        
        public void createServico(Servico s)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Servico.Add(s);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void updateServico(Servico s)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Servico.Where(x => x.id == s.id).SingleOrDefault().descricao = s.descricao;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void deleteServico(int id)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    ctx.Servico.Remove(ctx.Servico.Where(x => x.id == id).SingleOrDefault());
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