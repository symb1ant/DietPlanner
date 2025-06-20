using DietPlanner.Data;
using DietPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DietPlanner.API.Tests.Fixtures;
[TestClass]
public static class Database
{
    public static ApplicationDbContext GetDbContext()
        => new(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(Constants.CONNECTION_STRING).Options);

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext testContext)
    {
        Console.WriteLine(testContext.TestName);

        using var dbContext = GetDbContext();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        dbContext.AddAsync(new ApplicationUser { UserName = "test@test.com", Email = "test@test.com", Id = Constants.TEST_USER_ID });
        dbContext.AddAsync(new DietEntry { UserId = Constants.TEST_USER_ID, Date = DateTime.Now, Food = "Test Food", Calories = 100, MealTypeId = 1 });
        dbContext.AddAsync(new DietEntry { UserId = Constants.TEST_USER_ID, Date = DateTime.Now, Food = "Test Food For Deletion", Calories = 100, MealTypeId = 2 });

        dbContext.SaveChanges();
    }
}
