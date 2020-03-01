using Finance.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface ICategoryService : IService
    {
        Task AddCategoryAsync(string name, int userId, string iconName, string hexColor);

        Task AddSubcategoryAsync(string name, int userId, int parentId, string iconName, string hexColor);

        Task RemoveCategoryAsync(int userId, int categoryId);

        Task<CategoryDto> GetCategoryAsync(int categoryId);

        Task<IEnumerable<CategoryDto>> GetCategoriesByUserIdAsync(int userId);


    }
}
