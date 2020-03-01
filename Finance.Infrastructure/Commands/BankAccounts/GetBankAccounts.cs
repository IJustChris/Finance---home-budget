using Finance.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.BankAccounts
{
    public class GetBankAccounts: AuthenticatedCommandBase
    {
        //output property
        public IEnumerable<BankAccountDto> banks{ get; set; }
    }
}
