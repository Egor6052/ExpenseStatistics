using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExpenseStatistics.Dto;
using ExpenseStatistics.Services;
using System.Security.Claims;

namespace ExpenseStatistics.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(TransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // Створення нової транзакції (дохід або витрата)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
        {
            var userId = GetUserId();
            var transaction = await _transactionService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
        }

        // Отримання транзакції за ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var transaction = await _transactionService.GetByIdAsync(id, userId);
            return Ok(transaction);
        }

        // Метод для отримання ID користувача з токена
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
        }
    }
}
