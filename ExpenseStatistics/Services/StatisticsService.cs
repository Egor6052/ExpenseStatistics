using Microsoft.Extensions.Caching.Memory;
using ExpenseStatistics.Repositories;
using ExpenseStatistics.Dto;

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
            var cacheKey = $"statistics_{userId}_{startDate?.ToString("yyyyMMdd") ?? "all"}_{endDate?.ToString("yyyyMMdd") ?? "all"}";
            if (_cache.TryGetValue(cacheKey, out SummaryStatisticsDto? cachedResult) && cachedResult != null)
                return cachedResult;

            // TODO: Реалізувати асинхронну логіку отримання транзакцій з репозиторію
            var result = new SummaryStatisticsDto
            {
                TotalIncome = 0,
                TotalExpense = 0,
                Balance = 0
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            // Тимчасово додано await для уникнення попередження
            return await Task.FromResult(result); 
        }
    }
}