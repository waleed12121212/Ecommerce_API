using AutoMapper;
using Ecommerce.DTOs.Category;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;

        public CategoryController(IRepository<Category> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories( )
        {
            var categories = await repository.GetAllAsync();
            var categoryDtos = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            var categoryDto = mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);
            await repository.AddAsync(category);
            return CreatedAtAction(nameof(GetCategoryById) , new { id = category.Id } , mapper.Map<CategoryDto>(category));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(int id , CategoryUpdateDto dto)
        {
            var existingCategory = await repository.GetByIdAsync(id);
            if (existingCategory == null) return NotFound();

            mapper.Map(dto , existingCategory);
            await repository.UpdateAsync(existingCategory);
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
