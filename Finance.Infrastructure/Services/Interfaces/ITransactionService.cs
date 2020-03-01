using Finance.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface ITransactionService : IService
    {
        Task<TransactionDto> GetAsync(int transactionId);

        Task<IEnumerable<TransactionDto>> GetTransactionsByBankIdAsync(int bankId);

        Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId);

        Task AddTransactionAsync(string description, int categoryId, decimal value, DateTime date, int accountId, int userId);

        Task RemoveTransactionAsync(int transactionId);

        Task RemoveTransactionsByBankAccountIdAsync(int bankAccountId);

        Task RemoveTransactionsByCategoryIdAsync(int userId, int categoryId);

        Task RemoveTransactionsByUserIdAsync(int userId);
    }
}
