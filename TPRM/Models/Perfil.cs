using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TPRM.Models
{
    public class Perfil
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string adm { get; set; }
        public string analista { get; set; }

    }

    public class PerfilConfig : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfig()
        {
            ToTable("perfil");
            HasKey<Int32>(x => x.id);
            Property(x => x.descricao).IsRequired().HasMaxLength(200);
            Property(x => x.adm).HasMaxLength(1);
            Property(x => x.analista).HasMaxLength(1);
        }
    }
}