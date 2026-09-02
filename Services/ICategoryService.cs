using ExpenseTrackerApi.DTOs;

namespace ExpenseTrackerApi.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

        Task<bool> UpdateAsync(int id, CreateCategoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
