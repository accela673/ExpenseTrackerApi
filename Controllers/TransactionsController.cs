using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetAll(
            [FromQuery] int? categoryId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var transactions = await _service.GetAllAsync(
                categoryId,
                from,
                to);

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDto>> GetById(int id)
        {
            var transaction = await _service.GetByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> Create(
            CreateTransactionDto dto)
        {
            var transaction = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = transaction.Id },
                transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateTransactionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
