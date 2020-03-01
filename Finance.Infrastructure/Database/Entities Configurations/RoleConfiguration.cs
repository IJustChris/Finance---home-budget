using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.RoleId);

            builder.Property(x => x.RoleId)
                .HasColumnName("role_id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.RoleName)
                .HasColumnName("role_name")
                .HasColumnType("varchar(10)");
        }
    }
}
