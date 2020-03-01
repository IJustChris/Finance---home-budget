using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Categories;
using Finance.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Categories
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(CreateCategory command)
        {

            await _categoryService.AddCategoryAsync(command.Name, command.userId, command.IconName, command.ColorHex);
        }
    }
}
