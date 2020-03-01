using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId)
                .HasColumnName("transaction_id")
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(x => x.BankAccountId)
                .HasColumnName("bank_account_id")
                .IsRequired(true);

            builder.Property(x => x.CategoryId)
                .HasColumnName("category_id")
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(40)")
                .IsRequired(false);

            builder.Property(x => x.Value)
                .HasColumnName("value_id")
                .IsRequired(true);

            builder.Property(x => x.Date)
                .HasColumnName("date")
                .IsRequired(true);


        }
    }
}
