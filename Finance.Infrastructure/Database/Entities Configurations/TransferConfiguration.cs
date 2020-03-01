using Finance.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Infrastructure.Database.Entities_Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasBaseType<Transaction>();

            builder.Property(x => x.DestinationBankAccountId)
                .HasColumnName("destination_bank_account_id");

            builder.Property(x => x.SourceBankAccountId)
                .HasColumnName("source_bank_account_id");
        }
    }
}
