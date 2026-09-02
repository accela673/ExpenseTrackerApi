using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(
            int? categoryId,
            DateTime? from,
            DateTime? to);

        Task<Transaction?> GetByIdAsync(int id);

        Task AddAsync(Transaction transaction);

        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(Transaction transaction);
    }
}
