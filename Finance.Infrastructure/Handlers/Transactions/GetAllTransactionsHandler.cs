using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Transactions;
using Finance.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Transactions
{
    public class GetAllTransactionsHandler : ICommandHandler<GetAllTransactions>
    {
        private readonly ITransactionService _transactionsService;

        public GetAllTransactionsHandler(ITransactionService transactionService)
        {
            _transactionsService = transactionService;
        }

        public async Task HandleAsync(GetAllTransactions command)
        {
            command.transactions = await _transactionsService.GetTransactionsByUserIdAsync(command.userId);
        }
    }
}
