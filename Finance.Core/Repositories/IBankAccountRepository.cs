using Finance.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface IBankAccountRepository : IRepository
    {
        Task<BankAccount> GetAsync(int id);

        Task<IEnumerable<BankAccount>> GetAllBankAccountsByUserIdAsync(int userID);

        Task AddAsync(BankAccount bankAccount);

        Task RemoveAsync(BankAccount bankAccount);

        Task UpdateAsync(BankAccount bankAccount);

        Task<int> NextId();
    }
}
