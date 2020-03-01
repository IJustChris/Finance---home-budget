using System;

namespace Finance.Infrastructure.Commands.Transactions
{
    public class UpdateTransaction : AuthenticatedCommandBase
    {
        public int TransactioId { get; set; }
        public int SourceBankID { get; set; }
        public int DestinationAccountID { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
    }
}
