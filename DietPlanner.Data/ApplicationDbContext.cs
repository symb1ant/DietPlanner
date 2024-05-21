using DietPlanner.Data.Configurations;
using DietPlanner.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<DietEntry> DietEntries { get; set; }
    public DbSet<MealType> MealTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new DietEntryConfiguration());
        builder.ApplyConfiguration(new MealTypeConfiguration());

    }
}
