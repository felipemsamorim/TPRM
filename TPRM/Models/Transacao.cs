using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TPRM.Models
{
    public class Transacao :IAuditable
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public double preco { get; set; }
        public string preco_f { get { return preco.ToString("C"); } }
        public virtual Servico servico { get; set; }
        public virtual string servico_desc{ get {
                return servico.descricao;
            } }
        public virtual Empresa empresa_contratada { get; set; }
        public virtual string empresa_contratada_nome
        {
            get
            {
                return empresa_contratada.nomefantasia;
            }
        }
        public virtual Empresa empresa_contratante { get; set; }
        public virtual string empresa_contratante_nome
        {
            get
            {
                return empresa_contratante.nomefantasia;
            }
        }
        public string status { get; set; }
        public virtual string status_desc { get {
                switch (status)
                {
                    case "a":
                        return "Aprovada";
                    case "r":
                        return "Reprovada";
                    case "p":
                        return "Pendente";
                    default:
                        return "Pendente";
                }
            } }
        public DateTime CreatedDate { get; set; }
        public virtual string CreatedDate_f{get{ return CreatedDate.ToString("dd/MM/yyyy"); }}
        public String CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual string UpdatedDate_f{ get { return UpdatedDate.ToString("dd/MM/yyyy"); } }
        public String UpdatedBy { get; set; }
    }

    public class TransacaoConfig : EntityTypeConfiguration<Transacao>
    {
        public TransacaoConfig()
        {
            ToTable("transacao");
            HasKey<Int32>(x => x.id);
            Property(x => x.descricao).IsRequired().HasMaxLength(200);
            Property(x => x.preco).IsRequired();
            Property(x => x.CreatedDate);
            Property(x => x.CreatedBy);
            Property(x => x.UpdatedDate);
            Property(x => x.UpdatedBy);
            //a - aprovada, r - reprovada, p - pendente
            Property(x => x.status).HasMaxLength(1);
            HasRequired(x => x.servico);
            HasRequired(x => x.empresa_contratada);
            HasRequired(x => x.empresa_contratante);
        }
    }
}