using FCG.User.Domain.Entities;
using FCG.User.Infra.Data.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCG.User.Infra.Data.Context
{
    public class FCGDbContext : IdentityDbContext<Domain.Entities.User>
    {

        public FCGDbContext(DbContextOptions<FCGDbContext> options) : base(options)
        {
        }

        public DbSet<UserGameLibrary> UserGameLibraries => Set<UserGameLibrary>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());

            modelBuilder.ApplyConfiguration(new UserGameLibraryMapping());
        }

    }
}
