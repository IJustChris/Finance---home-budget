using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Transactions;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Transactions
{
    public class DeleteTransactionHandler : ICommandHandler<DeleteTransaction>
    {
        private readonly ITransactionService _transactionsService;

        public DeleteTransactionHandler(ITransactionService transactionService)
        {
            _transactionsService = transactionService;
        }

        public async Task HandleAsync(DeleteTransaction command)
        {
            await _transactionsService.RemoveTransactionAsync(command.TransactionId);
        }
    }
}
