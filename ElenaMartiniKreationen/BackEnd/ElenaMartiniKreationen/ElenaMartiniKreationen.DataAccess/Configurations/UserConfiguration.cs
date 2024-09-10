using ElenaMartiniKreationen.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(x => x.FirstName)
                  .HasMaxLength(200);

            builder.Property(x => x.FirstName)
                 .HasMaxLength(200);

            builder.Property(x => x.LastName)
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsUnicode(false)
                .HasMaxLength(500);
        }
    }
}
