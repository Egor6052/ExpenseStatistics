using ExpenseStatistics.Domain.Entities;

namespace ExpenseStatistics.Services
{
    public class CategoryService
    {
        // Тут має бути реальна логіка роботи з категоріями
        public Task<Category> CreateAsync(string name, Guid userId)
        {
            // Тут буде додавання в базу даних (тимчасово можемо зробити заглушку)
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId
            };

            // Повертаємо асинхронний результат
            return Task.FromResult(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync(Guid userId)
        {
            // Поки що повертаємо фейковий список
            var categories = new List<Category>();
            return Task.FromResult(categories.AsEnumerable());
        }
    }
}
