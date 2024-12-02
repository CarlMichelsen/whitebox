using Database;
using Microsoft.EntityFrameworkCore;

namespace Test.Fake;

public static class TestDatabaseContextFactory
{
    public static ApplicationContext Create(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        
        return new ApplicationContext(options);
    }
}