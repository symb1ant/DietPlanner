using DietPlanner.Data.Interfaces;
using DietPlanner.Data.Models;
using DietPlanner.Data;
using DietPlanner.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Services;
public static class StartupExtensions
{
    public static void AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository<DietEntry>, GenericRepository<DietEntry, ApplicationDbContext>>();
        services.AddScoped<IRepository<MealType>, GenericRepository<MealType, ApplicationDbContext>>();
        services.AddScoped<IRepository<ApplicationUser>, GenericRepository<ApplicationUser, ApplicationDbContext>>();
    }
}
