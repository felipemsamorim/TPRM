using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace TPRM.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public virtual Perfil perfil { get; set; }
        public string prefil_desc { get { return perfil == null ? "" : perfil.descricao; } }
        public virtual Empresa empresa { get; set; }
        public string empresa_nome { get { return empresa == null ? "" : empresa.nomefantasia; } }

    }

    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable("usuario");
            HasKey<Int32>(x => x.id);
            Property(x => x.login).IsRequired().HasMaxLength(50);
            Property(x => x.senha).IsRequired().HasMaxLength(50);
            HasOptional<Perfil>(x => x.perfil);
            HasOptional<Empresa>(x => x.empresa);
        }
    }
        
    
}