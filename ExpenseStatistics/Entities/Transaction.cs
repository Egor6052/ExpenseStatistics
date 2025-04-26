using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseStatistics.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [Required]
        // "income" або "expense"
        public string Type { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
