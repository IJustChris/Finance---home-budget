using Finance.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface ITransactionRepository : IRepository
    {
        Task<Transaction> GetAsync(int id);

        Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId);

        Task<IEnumerable<Transaction>> GetAllTransactionsByBankId(int bankId);

        Task<IEnumerable<Transaction>> GetAllTransactionsByCategoryId(int userId, int categoryId);

        Task AddAsync(Transaction transaction);

        Task RemoveAsync(Transaction transaction);

        Task UpdateAsync(Transaction transaction);

        Task<int> NextId();
    }
}
