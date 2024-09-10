using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.DataAccess.Data
{
    public class ElenaMartiniKreationenDbContext : IdentityDbContext<IdentityUserElenaMartiniKreationen>
    {
        public ElenaMartiniKreationenDbContext(DbContextOptions<ElenaMartiniKreationenDbContext> options) : base(options){ }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElenaMartiniKreationenDbContext).Assembly);

            // AspNetUsers
            modelBuilder.Entity<IdentityUserElenaMartiniKreationen>(e =>
            {
                e.ToTable("User");
            });

            // AspNetRoles
            modelBuilder.Entity<IdentityRole>(e =>
            {
                e.ToTable("Role");
            });

            // AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>(e =>
            {
                e.ToTable("UserRole");
            });

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
        }


    }
}
