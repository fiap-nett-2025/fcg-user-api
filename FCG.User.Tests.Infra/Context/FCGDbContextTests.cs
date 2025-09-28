using FCG.User.Domain.Entities;
using FCG.User.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.User.Tests.Infra.Context;
public class FCGDbContextTests
{
    [Fact]
    public void CanSaveUserWithEmail()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        using (var context = new FCGDbContext(options))
        {
            var user = new User.Domain.Entities.User("Test", "valid@fiap.com.br");
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new FCGDbContext(options))
        {
            Assert.Equal(1, context.Users.Count());
        }
    }
}