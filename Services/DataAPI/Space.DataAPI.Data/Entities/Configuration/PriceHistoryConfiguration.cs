using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Space.DataAPI.Data.Entities.Configuration
{
    public class PriceHistoryConfiguration : IEntityTypeConfiguration<PriceHistory>
    {
        public void Configure(EntityTypeBuilder<PriceHistory> builder)
        {
            builder.HasKey(q => q.Id);
        }
    }
}