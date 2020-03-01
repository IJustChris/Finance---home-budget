using AutoMapper;
using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.DTO;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;




        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, ITransactionService transactionService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _transactionService = transactionService;
        }


        public async Task AddCategoryAsync(string name, int userId, string iconName, string hexColor)
        {
            //var user = await _userRepository.GetOrFailAsync(userId);

            var category = Category.Create(await _categoryRepository.NextId(), name, userId, iconName, hexColor);


            await _categoryRepository.AddAsync(category);
        }

        public async Task AddSubcategoryAsync(string name, int userId, int parentId, string iconName, string hexColor)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            var parent = await _categoryRepository.GetAsync(parentId);

            if (parent.CategoryId != parent.ParentId)
            {
                throw new Exception($"Cant create Subcategory becouse parent with id: {parentId} is already subcategory");
            }

            if (user.UserId != parent.UserId)
            {
                throw new Exception($"Cant create Subcategory becouse category belongs to other user");
            }


            var category = parent.CreateSubcategory(await _categoryRepository.NextId(), name, iconName, hexColor);

            await _categoryRepository.AddAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByUserIdAsync(int userId)
        {
            var categories = await _categoryRepository.GetAllCategoriesByUserIdAsync(userId);
            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                var dto = _mapper.Map<CategoryDto>(category);
                categoriesDto.Add(dto);
            }

            return categoriesDto;
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(categoryId);

            if (category == null)
            {
                throw new Exception("Category was not found");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task RemoveCategoryAsync(int userId, int categoryId)
        {
            var category = await _categoryRepository.GetAsync(categoryId);

            if (category == null)
            {
                throw new Exception("Category was not found");
            }

            await _transactionService.RemoveTransactionsByCategoryIdAsync(userId, category.CategoryId);
            await _categoryRepository.RemoveAsync(category);
        }


    }
}
