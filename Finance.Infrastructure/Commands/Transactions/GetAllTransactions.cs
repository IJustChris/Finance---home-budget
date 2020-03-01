using Finance.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Transactions
{
    public class GetAllTransactions: AuthenticatedCommandBase
    {
        //output
        public IEnumerable<TransactionDto> transactions;
    }
}
