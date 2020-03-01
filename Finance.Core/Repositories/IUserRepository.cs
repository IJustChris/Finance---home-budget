using Finance.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(string email);

        Task<User> GetAsync(int id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task AddAsync(User user);

        Task RemoveAsync(User user);

        Task UpdateAsync(User user);

        Task<int> NextId();
    }
}
