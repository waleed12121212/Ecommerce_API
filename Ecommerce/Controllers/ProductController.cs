using AutoMapper;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Repositories.ProductRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProducts( )
        {
            var products = await repository.GetAllWithCategoriesAsync();
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            Console.WriteLine($"GetProductById called with id = {id}");
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            var productDto = mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto dto)
        {
            var product = mapper.Map<Product>(dto);
            await repository.AddAsync(product);
            return CreatedAtAction(nameof(GetProductById) , new { id = product.Id } , mapper.Map<ProductDto>(product));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id , ProductUpdateDto dto)
        {
            var existingProduct = await repository.GetByIdAsync(id);
            if (existingProduct == null) return NotFound();

            mapper.Map(dto , existingProduct);
            await repository.UpdateAsync(existingProduct);
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
