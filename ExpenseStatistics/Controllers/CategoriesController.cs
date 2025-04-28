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
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // Створення нової категорії
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var userId = GetUserId();
            var category = await _categoryService.CreateAsync(dto.Name, userId);
            return Ok(category);
        }

        // Отримання всіх категорій користувача
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var categories = await _categoryService.GetAllAsync(userId);
            return Ok(categories);
        }

        // Метод для безпечного отримання ID користувача з токена
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
        }
    }
}
