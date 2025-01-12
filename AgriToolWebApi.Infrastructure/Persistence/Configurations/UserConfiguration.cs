using AgriToolWebApi.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgriToolWebApi.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserPersistenceEntity>
    {
        public void Configure(EntityTypeBuilder<UserPersistenceEntity> builder)
        {
            builder.ToTable("users");

            builder.Property(e => e.Uuid)
                   .HasColumnType("char(36)")
                   .IsRequired();
        }
    }
}
