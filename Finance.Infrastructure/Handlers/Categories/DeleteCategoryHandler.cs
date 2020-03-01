using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Categories;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Handlers.Categories
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(DeleteCategory command)
        {
            await _categoryService.RemoveCategoryAsync(command.userId,command.CategoryId);
        }
    }
}
