using ExpenseStatistics.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseStatistics.Dto
{
    public class CreateTransactionDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        // Enum: Income або Expense
        public TransactionType Type { get; set; } 

        [StringLength(200, ErrorMessage = "Description can't be longer than 200 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
