using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TPRM.Models
{
    public class Empresa
    {
        public int id { get; set; }
        public string cnpj { get; set; }
        public string razaosocial { get; set; }
        public string nomefantasia { get; set; }
    }

    public class EmpresaConfig : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfig()
        {
            ToTable("empresa");
            HasKey<Int32>(x => x.id);
            Property(x => x.cnpj).IsRequired().HasMaxLength(18);
            Property(x => x.razaosocial).IsRequired().HasMaxLength(255);
            Property(x => x.nomefantasia).IsRequired().HasMaxLength(255);
        }
    }
}