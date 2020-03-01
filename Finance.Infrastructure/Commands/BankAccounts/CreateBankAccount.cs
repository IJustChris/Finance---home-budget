namespace Finance.Infrastructure.Commands.BankAccounts
{
    public class CreateBankAccount : AuthenticatedCommandBase
    {
        public string BankAccountName { get; set; }
        public decimal InitialBalance { get; set; }
        public string HexColor { get; set; }
        public string Currency { get; set; }
    }
}
