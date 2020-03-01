using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.BankAccounts
{
    public class DeleteBankAccountHandler : ICommandHandler<DeleteBankAccount>
    {
        private readonly IBankAccountService _bankAccountService;

        public DeleteBankAccountHandler(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public async Task HandleAsync(DeleteBankAccount command)
        {
           await _bankAccountService.RemoveBankAccountAsync(command.BankAccountId);
        }
    }
}
