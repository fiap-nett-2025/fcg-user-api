using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infra.Data.Mappings
{
    public class UserGameLibraryMapping : IEntityTypeConfiguration<UserGameLibrary>
    {
        public void Configure(EntityTypeBuilder<UserGameLibrary> builder)
        {
            builder.ToTable("UserGameLibrary");

            // PK composta (UserId + GameId) evita duplicatas
            builder.HasKey(x => new { x.UserId, x.GameId });

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(452);

            builder.Property(x => x.GameId).IsRequired();

            builder.Property(x => x.AddedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd(); 
        }
    }
}
