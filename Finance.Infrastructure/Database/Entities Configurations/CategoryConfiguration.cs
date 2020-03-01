using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);

            builder.Property(x => x.CategoryId)
                .HasColumnName("category_id")
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(x => x.ParentId)
                .HasColumnName("parent_id")
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            builder.Property(x => x.BudgetId)
                .HasColumnName("budget_id");

            builder.Property(x => x.IconName)
                .HasColumnName("icon_name")
                .HasColumnType("varchar").HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.ColorHex)
                .HasColumnName("color_hex")
                .HasColumnType("varchar").HasMaxLength(7)
                .IsRequired(true);

            builder.Property(x => x.Name).HasColumnName("name")
                .HasColumnType("varchar(20)")
                .IsRequired(true);

        }
    }
}
