using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FCG.User.Domain.Entities;
using FCG.User.Domain.ValueObjects;

namespace FCG.User.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable("AspNetUsers"); // 🔥 Mantém o padrão Identity

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            
        }
    }
}
