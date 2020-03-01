using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.BankAccounts
{
    public class UpdateBankAccountHandler : ICommandHandler<UpdateBankAccount>
    {
        private IBankAccountService _bankAccountService;

        public UpdateBankAccountHandler(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public async Task HandleAsync(UpdateBankAccount command)
        {
            var dto = new BankAccountDto
            {
                BanAccountId = command.BankAccountId,
                AccountName = command.BankAccountName,
                Currency = command.Currency,
                InitialBalance = command.InitialBalance,
                HexColor = command.HexColor
            };

            await _bankAccountService.UpdateAsync(dto);
        }
    }
}
