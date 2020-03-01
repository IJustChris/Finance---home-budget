using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.BanAccountId);

            builder.Property(x => x.BanAccountId)
                .HasColumnName("bank_account_id")
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            builder.Property(x => x.AccountName).HasColumnName("account_name")
                .HasColumnType("varchar(20)")
                .IsRequired(true);

            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .HasColumnType("varchar(3)")
                .IsRequired(true);

            builder.Property(x => x.HexColor)
                .HasColumnName("hex_color")
                .HasColumnType("varchar(7)")
                .IsRequired(true);

            builder.Property(x => x.InitialBalance)
                .HasColumnName("initial_balance")
                .IsRequired(true);

            builder.Property(x => x.Balance)
                .HasColumnName("balance");


        }
    }
}
