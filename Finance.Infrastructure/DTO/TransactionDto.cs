using System;

namespace Finance.Infrastructure.DTO
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int BankAccountId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
    }
}
