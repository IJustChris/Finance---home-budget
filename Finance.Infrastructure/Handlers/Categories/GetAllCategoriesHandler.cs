using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Categories;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Categories
{
    public class GetAllCategoriesHandler : ICommandHandler<GetAllCategories>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(GetAllCategories command)
        {
            command.categories = await _categoryService.GetCategoriesByUserIdAsync(command.userId);
        }
    }
}
