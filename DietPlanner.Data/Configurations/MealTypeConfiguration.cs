using DietPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Data.Configurations;
public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
{
    public void Configure(EntityTypeBuilder<MealType> builder)
    {
        builder.HasKey(br => br.Id);

        builder.HasData(
            new MealType { Id = 1, Name = "Breakfast" },
            new MealType { Id = 2, Name = "Lunch" },
            new MealType { Id = 3, Name = "Dinner" },
            new MealType { Id = 4, Name = "Snack" });
    }
}
