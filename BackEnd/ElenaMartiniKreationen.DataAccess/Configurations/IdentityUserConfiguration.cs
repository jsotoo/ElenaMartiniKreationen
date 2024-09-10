using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.DataAccess.Configurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUserElenaMartiniKreationen>
    {
        

        public void Configure(EntityTypeBuilder<IdentityUserElenaMartiniKreationen> builder)
        {
            builder.Property(p => p.FullName)
                .HasMaxLength(200);
            builder.Property(p => p.Address)
                .HasMaxLength(500);
        }
    }
}
