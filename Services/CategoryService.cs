using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Repositories;

namespace ExpenseTrackerApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var existingCategory = await _repository.GetByNameAsync(dto.Name);

            if (existingCategory != null)
                throw new InvalidOperationException("Category with this name already exists.");

            var category = new Category
            {
                Name = dto.Name
            };

            await _repository.AddAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            var existingCategory = await _repository.GetByNameAsync(dto.Name);

            if (existingCategory != null && existingCategory.Id != id)
                throw new InvalidOperationException("Category with this name already exists.");

            category.Name = dto.Name;

            await _repository.UpdateAsync(category);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            if (category.Transactions.Any())
                throw new InvalidOperationException(
                    "Cannot delete category because it has transactions.");

            await _repository.DeleteAsync(category);

            return true;
        }
    }
}
