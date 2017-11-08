using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TPRM.Models
{
    public class Servico
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }

    public class ServicoConfig : EntityTypeConfiguration<Servico>
    {
        public ServicoConfig()
        {
            ToTable("servico");
            HasKey<Int32>(x => x.id);
            Property(x => x.descricao).IsRequired().HasMaxLength(200);
        }
    }
}