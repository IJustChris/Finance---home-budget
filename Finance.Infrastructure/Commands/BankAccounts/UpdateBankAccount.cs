using System;

namespace Finance.Infrastructure.Commands.BankAccounts
{
    public class UpdateBankAccount : AuthenticatedCommandBase
    {
        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public decimal InitialBalance { get; set; }
        public string HexColor { get; set; }
        public string Currency { get; set; }

    }
}
