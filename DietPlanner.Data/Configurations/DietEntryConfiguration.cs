using DietPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Data.Configurations;
partial class DietEntryConfiguration : IEntityTypeConfiguration<Models.DietEntry>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Models.DietEntry> builder)
    {
        builder.HasKey(br => br.Id);

        builder.Property(br => br.Food)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(br => br.Calories)
            .IsRequired();

        builder.Property(br => br.Date)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(a => a.User)
            .WithMany(u => u.DietEntries)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.MealType)
            .WithMany(mt => mt.DietEntries)
            .HasForeignKey(a => a.MealTypeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
