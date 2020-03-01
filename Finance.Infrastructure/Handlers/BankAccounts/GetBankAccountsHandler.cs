using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.BankAccounts
{
    public class GetBankAccountsHandler : ICommandHandler<GetBankAccounts>
    {
        private readonly IBankAccountService _bankAccountService;

        public GetBankAccountsHandler(IMemoryCache cache, IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public async Task HandleAsync(GetBankAccounts command)
        {
            var accounts = await _bankAccountService.GetAllBankAccountsByUserIdAsync(command.userId);

            command.banks = accounts;
        }
    }
}
