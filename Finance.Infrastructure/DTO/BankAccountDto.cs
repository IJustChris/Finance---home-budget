using System.Collections.Generic;

namespace Finance.Infrastructure.DTO
{
    public class BankAccountDto
    {
        public int BanAccountId { get; set; }
        public int UserId { get; set; }
        public string AccountName { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string HexColor { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }

    }
}
