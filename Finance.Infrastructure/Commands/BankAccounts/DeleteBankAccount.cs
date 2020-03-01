using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.BankAccounts
{
    public class DeleteBankAccount: AuthenticatedCommandBase
    {
        public int BankAccountId { get; set; }
    }
}
