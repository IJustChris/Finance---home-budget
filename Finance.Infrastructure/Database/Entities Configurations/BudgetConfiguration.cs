using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasKey(x => x.BudgetId);

            builder.Property(x => x.BudgetId)
                .HasColumnName("budget_id")
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired(true);

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(20)")
                .IsRequired(true);

            builder.Property(x => x.ColorHex)
                .HasColumnName("color_hex")
                .HasColumnType("varchar(7)")
                .IsRequired(true);

            builder.Property(x => x.IconName)
                .HasColumnName("icon_name")
                .HasColumnType("varchar(20)")
                .IsRequired(true);

            builder.Property(x => x.BudgetLimit).HasColumnName("budget_limit").IsRequired(true);
            builder.Property(x => x.ActualExpenditure).HasColumnName("actual_expanditure").IsRequired(true);
        }
    }
}
