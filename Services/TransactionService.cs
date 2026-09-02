using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Repositories;

namespace ExpenseTrackerApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<TransactionDto>> GetAllAsync(
            int? categoryId,
            DateTime? from,
            DateTime? to)
        {
            var transactions = await _transactionRepository
                .GetAllAsync(categoryId, from, to);

            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Description = t.Description,
                Date = t.Date,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name
            }).ToList();
        }

        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
                return null;

            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category.Name
            };
        }

        public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                throw new InvalidOperationException(
                    "Category with this id does not exist.");

            var transaction = new Transaction
            {
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date,
                CategoryId = dto.CategoryId
            };

            await _transactionRepository.AddAsync(transaction);

            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = category.Name
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            CreateTransactionDto dto)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
                return false;

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                throw new InvalidOperationException(
                    "Category with this id does not exist.");

            transaction.Amount = dto.Amount;
            transaction.Description = dto.Description;
            transaction.Date = dto.Date;
            transaction.CategoryId = dto.CategoryId;

            await _transactionRepository.UpdateAsync(transaction);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);

            if (transaction == null)
                return false;

            await _transactionRepository.DeleteAsync(transaction);

            return true;
        }
    }
}
