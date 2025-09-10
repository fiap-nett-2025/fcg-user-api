using FCG.Domain.Entities;
using FCG.Infra.Data.Mappings;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Context
{
    public class FCGDbContext : IdentityDbContext<User>
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
