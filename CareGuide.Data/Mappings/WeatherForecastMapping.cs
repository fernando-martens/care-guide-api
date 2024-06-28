using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class WeatherForecastMapping : IEntityTypeConfiguration<WeatherForecastTable>
    {
        public void Configure(EntityTypeBuilder<WeatherForecastTable> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Summary).HasMaxLength(200);
        }
    }
}
