using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseStatistics.Services;
using System.Security.Claims;

namespace ExpenseStatistics.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // Отримання зведеної фінансової статистики
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = GetUserId();
            var summary = await _statisticsService.GetSummaryAsync(userId, startDate, endDate);
            return Ok(summary);
        }

        // Отримання детальної фінансової статистики
        [HttpGet("detailed")]
        public async Task<IActionResult> GetDetailed([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = GetUserId();
            var detailedStats = await _statisticsService.GetDetailedAsync(userId, startDate, endDate);
            return Ok(detailedStats);
        }

        // Отримання ID користувача з токена
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
        }
    }
}
