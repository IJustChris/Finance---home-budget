using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.Services;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.BankAccounts
{
    public class CreateBankAccountHandler : ICommandHandler<CreateBankAccount>
    {
        private readonly IBankAccountService _bankAccountService;

        public CreateBankAccountHandler(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }


        public async Task HandleAsync(CreateBankAccount command)
        {
            await _bankAccountService.CreateNewBankAccountAsync(command.userId, command.BankAccountName, command.InitialBalance, command.Currency, command.HexColor);
        }
    }
}
