using FCG.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FCG.User.Infra.Data.Mappings
{
    public class UserGameLibraryMapping : IEntityTypeConfiguration<UserGameLibrary>
    {
        public void Configure(EntityTypeBuilder<UserGameLibrary> builder)
        {
            builder.ToTable("UserGameLibrary");

            // PK composta (UserId + GameId) evita duplicatas
            builder.HasKey(x => new { x.UserId, x.GameId });

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(450);

            builder.Property(x => x.GameId).IsRequired();

            builder.Property(x => x.AddedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.User)
               .WithMany() // ou .WithMany(u => u.GameLibrary) se adicionou a coleção no User
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
