using Microsoft.Extensions.Caching.Memory;
using ExpenseStatistics.Repositories;
using ExpenseStatistics.Dto;
using System.Linq;

namespace ExpenseStatistics.Services
{
    public class StatisticsService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMemoryCache _cache;

        public StatisticsService(TransactionRepository transactionRepository, IMemoryCache cache)
        {
            _transactionRepository = transactionRepository;
            _cache = cache;
        }

        public async Task<SummaryStatisticsDto> GetSummaryAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var cacheKey = $"summary_{userId}_{startDate?.ToString("yyyyMMdd") ?? "all"}_{endDate?.ToString("yyyyMMdd") ?? "all"}";
            if (_cache.TryGetValue(cacheKey, out SummaryStatisticsDto? cachedResult) && cachedResult != null)
                return cachedResult;

            var transactions = await _transactionRepository.GetByUserIdAsync(userId, startDate, endDate);

            var income = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);

            var result = new SummaryStatisticsDto
            {
                TotalIncome = income,
                TotalExpense = Math.Abs(expense),
                Balance = income + expense
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }

        public async Task<IEnumerable<TransactionDto>> GetDetailedAsync(Guid userId, DateTime? startDate, DateTime? endDate)
        {
            var cacheKey = $"detailed_{userId}_{startDate?.ToString("yyyyMMdd") ?? "all"}_{endDate?.ToString("yyyyMMdd") ?? "all"}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<TransactionDto>? cachedResult) && cachedResult != null)
                return cachedResult;

            var transactions = await _transactionRepository.GetByUserIdAsync(userId, startDate, endDate);

            var detailed = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Description = t.Description,
                CategoryId = t.CategoryId,
                Date = t.Date
            }).ToList();

            _cache.Set(cacheKey, detailed, TimeSpan.FromMinutes(10));
            return detailed;
        }
    }
}
