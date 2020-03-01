using Finance.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public interface IBankAccountService : IService
    {
        Task CreateNewBankAccountAsync(int userId, string accountName, decimal initialBalance, string currency, string hexColor);

        Task<IEnumerable<BankAccountDto>> GetAllBankAccountsByUserIdAsync(int userId);

        Task RemoveBankAccountAsync(int id);

        Task<BankAccountDto> GetAsync(int id);

        Task UpdateAsync(BankAccountDto dto);

    }
}
