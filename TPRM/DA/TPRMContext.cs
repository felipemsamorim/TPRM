using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TPRM.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Npgsql;

namespace TPRM.DA
{
    public class TPRMContext : DbContext
    {
        public TPRMContext (): base("name=TPRM")
        {
            //Database.Connection.Open();
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Transacao> Transacao { get; set; }



        protected override void OnModelCreating(DbModelBuilder dbModel) {
            dbModel.HasDefaultSchema("public");
            base.OnModelCreating(dbModel);
            dbModel.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IAuditable>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {

                    string currentUser = HttpContext.Current.Request.Cookies["usuario"].Value;
                    auditableEntity.Entity.UpdatedDate = DateTime.Now;
                    auditableEntity.Entity.UpdatedBy = currentUser;
                    
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedDate = DateTime.Now;
                        auditableEntity.Entity.CreatedBy = currentUser;
                    }
                    else
                    {
                        auditableEntity.Property(p => p.CreatedDate).IsModified = false;
                        auditableEntity.Property(p => p.CreatedBy).IsModified = false;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}