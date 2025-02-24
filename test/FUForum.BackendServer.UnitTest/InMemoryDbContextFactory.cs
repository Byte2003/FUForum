using FUForum.BackendServer.Data;
using Microsoft.EntityFrameworkCore;

namespace FUForum.BackendServer.UnitTest;

public class InMemoryDbContextFactory
{
    
    public ApplicationDbContext GetApplicationDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDbForTesting")
            .Options;
        var dbContext = new ApplicationDbContext(options);
        return dbContext;
    }
}