using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllAsync(
            int? categoryId,
            DateTime? from,
            DateTime? to)
        {
            var query = _context.Transactions
                .Include(t => t.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (from.HasValue)
            {
                query = query.Where(t => t.Date >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(t => t.Date <= to.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
