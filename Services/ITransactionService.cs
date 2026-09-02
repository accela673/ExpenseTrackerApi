using ExpenseTrackerApi.DTOs;

namespace ExpenseTrackerApi.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetAllAsync(
            int? categoryId,
            DateTime? from,
            DateTime? to);

        Task<TransactionDto?> GetByIdAsync(int id);

        Task<TransactionDto> CreateAsync(CreateTransactionDto dto);

        Task<bool> UpdateAsync(int id, CreateTransactionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
