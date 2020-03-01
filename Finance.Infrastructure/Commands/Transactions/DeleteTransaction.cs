using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Transactions
{
    public class DeleteTransaction: AuthenticatedCommandBase
    {
        public int TransactionId { get; set; }
    }
}
