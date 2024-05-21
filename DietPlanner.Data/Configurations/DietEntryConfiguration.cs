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
    }
}
