using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {


            builder.HasKey(p => p.UserId);

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .ValueGeneratedNever()
                .IsRequired(true);


            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(40)")
                .IsRequired(true);


            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(200)")
                .IsRequired(true);


            builder.Property(x => x.Salt)
                .HasColumnName("salt")
                .HasColumnType("varchar(56)")
                .IsRequired(true);


            builder.Property(x => x.Username)
                .HasColumnName("username")
                .HasColumnType("varchar(20)")
                .IsRequired(false);


            builder.Property(x => x.Firstname)
                .HasColumnName("firstname")
                .HasColumnType("varchar(20)")
                .IsRequired(false);


            builder.Property(x => x.Lastname)
                .HasColumnName("lastname")
                .HasColumnType("varchar(20)")
                .IsRequired(false);

            builder.Property(x => x.RoleId)
                .HasColumnName("role_id");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at");


            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");
        }


    }
}
