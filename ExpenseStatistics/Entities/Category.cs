using System.ComponentModel.DataAnnotations;

namespace ExpenseStatistics.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
