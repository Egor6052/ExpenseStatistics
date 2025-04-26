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

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var userId = GetUserId();
            var category = await _categoryService.CreateAsync(dto.Name, userId);
            return Ok(category);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User ID not found"));
            var categories = await _categoryService.GetAllAsync(userId);
            return Ok(categories);
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst("id")?.Value ?? throw new Exception("User ID not found"));
        }

    }
}
