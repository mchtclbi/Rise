using Microsoft.EntityFrameworkCore;
using Space.UserAPI.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Space.UserAPI.Data.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(q => q.Id);
        }
    }
}