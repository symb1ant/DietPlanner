using Microsoft.Extensions.Configuration;

namespace DietPlanner.Repositories;
public static class StartupExtensions
{
    public static void AddDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<DietPlannerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
