using ExpenseStatistics.Domain.Entities;

namespace ExpenseStatistics.Services
{
    public class AuthService
    {
        public Task<User> RegisterAsync(string email, string password)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email
            };

            return Task.FromResult(user);
        }

        public Task<string> LoginAsync(string email, string password)
        {
            // TODO Тут має бути реальна логіка авторизації і генерація токена
            // Тимчасово тут фейковий токен
            var token = "fake-jwt-token";
            return Task.FromResult(token);
        }
    }
}
