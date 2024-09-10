using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElenaMartiniKreationen.Entities;

namespace ElenaMartiniKreationen.DataAccess.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.Property(x => x.Description)
            .HasMaxLength(70);

            builder.HasData(new List<UserType>
            {
                new() { Id = 1, Description = "Cliente Normal" },
                new() { Id = 2, Description = "Cliente Especial" },
            });
        }
    }
}
