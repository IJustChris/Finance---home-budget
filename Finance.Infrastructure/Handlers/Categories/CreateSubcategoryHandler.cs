using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Categories;
using Finance.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Categories
{
    class CreateSubcategoryHandler : ICommandHandler<CreateSubcategory>
    {
        private readonly ICategoryService _categoryService;

        public CreateSubcategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(CreateSubcategory command)
        {
            await _categoryService.AddSubcategoryAsync(command.CategoryName, command.userId, command.ParentId, command.IconName, command.HexColor);
        }
    }
}
