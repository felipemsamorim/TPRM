using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPRM.DA;
using TPRM.Models;

namespace TPRM.Contexts
{
    public class TransacaoCtx
    {
        public class TransacaoFaturamento
        {

            public string empresa_contratante { get; set; }
            public string valor { get; set; }
        }

        public List<TransacaoFaturamento> getTransacoesFaturamento(){
            var ctx = new TPRMContext();
            var faturamento = new List<TransacaoFaturamento>();
            var empresas = ctx.Empresa.ToList();
            var transacoes = ctx.Transacao.ToList();

            var total = from e in empresas
                        join t in transacoes
                        on e.id equals t.empresa_contratante.id
                        where t.status == "a"
                        group e by e.id
                        into g
                        select new TransacaoFaturamento()
                        {
                            empresa_contratante = g.FirstOrDefault().nomefantasia,
                            valor = (g.Count() * 5).ToString("C")
                        };
            return total.ToList();
        }
        public List<Transacao> getTransacaoAll()
        {
            List<Transacao> transacoes = new List<Transacao>();
            TPRMContext ctx = new TPRMContext();
            transacoes = ctx.Transacao.ToList();
            return transacoes;
        }
        public void Aprovar (int id)
        {
            using (TPRMContext ctx = new TPRMContext())
            {
                try
                {
                    ctx.Transacao.Where(t => t.id == id).SingleOrDefault().status = "a";
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void Reprovar(int id)
        {
            using (TPRMContext ctx = new TPRMContext())
            {
                try
                {
                    ctx.Transacao.Where(t => t.id == id).SingleOrDefault().status = "r";
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }
        public void Pendente(int id)
        {
            using (TPRMContext ctx = new TPRMContext())
            {
                try
                {
                    ctx.Transacao.Where(t => t.id == id).SingleOrDefault().status = "p";
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ctx.Dispose();
                    throw ex;
                }
            }
        }

        public void Salvar(Transacao t)
        {
            using (var ctx = new TPRMContext())
            {
                try
                {
                    t.empresa_contratada = ctx.Empresa.Where(x => x.id == t.empresa_contratada.id).SingleOrDefault();
                    t.empresa_contratante= ctx.Empresa.Where(x => x.id == t.empresa_contratante.id).SingleOrDefault();
                    t.servico= ctx.Servico.Where(x => x.id == t.servico.id).SingleOrDefault();
                    ctx.Transacao.Add(t);
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