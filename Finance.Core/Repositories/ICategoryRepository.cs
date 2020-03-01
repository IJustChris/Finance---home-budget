using Finance.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<Category> GetAsync(int id);

        Task<IEnumerable<Category>> GetAllCategoriesByUserIdAsync(int id);

        Task AddAsync(Category category);

        Task RemoveAsync(Category category);

        Task UpdateAsync(Category category);

        Task<int> NextId();
    }
}
