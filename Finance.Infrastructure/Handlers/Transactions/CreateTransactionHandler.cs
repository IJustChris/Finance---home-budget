using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Transactions;
using Finance.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Transactions
{
    public class CreateTransactionHandler : ICommandHandler<CreateTransaction>
    {
        private readonly ITransactionService _transactionsService;

        public CreateTransactionHandler(ITransactionService transactionService)
        {
            _transactionsService = transactionService;
        }

        public async Task HandleAsync(CreateTransaction command)
        {
            await _transactionsService.AddTransactionAsync(command.Description, command.CategoryId, command.Value, command.Date, command.SourceBankID, command.userId);
        }
    }
}
