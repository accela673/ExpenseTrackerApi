using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
