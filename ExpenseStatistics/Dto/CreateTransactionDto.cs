using ExpenseStatistics.Domain.Enums;

namespace ExpenseStatistics.Dto
{
    public class CreateTransactionDto
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
    }
}