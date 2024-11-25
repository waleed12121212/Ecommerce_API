using AutoMapper;
using Ecommerce.DTOs.Wishlist;
using Ecommerce.Models;
using Ecommerce.Repositories.WishlistRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper mapper;

        public WishlistController(IWishlistRepository repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: /wishlist/{customerId}
        [HttpGet("getAll")]
        public async Task<IActionResult> GetWishlist(int customerId)
        {
            // استرجاع قائمة التفضيلات للعميل
            var wishlistItems = await repository.GetWishlistByCustomerIdAsync(customerId);
            if (wishlistItems == null || !wishlistItems.Any())
                return NotFound("Wishlist is empty.");

            // تحويل البيانات إلى DTO
            var wishlistDtos = mapper.Map<IEnumerable<WishlistDto>>(wishlistItems);
            return Ok(wishlistDtos);
        }

        // POST: /wishlist
        [HttpPost]
        public async Task<IActionResult> CreateWishlist(WishlistCreateDto dto)
        {
            // التحقق من صحة البيانات المرسلة
            if (dto == null || dto.CustomerId <= 0 || dto.ProductId <= 0)
                return BadRequest("Invalid wishlist data.");

            // إضافة عنصر جديد إلى قائمة التفضيلات
            var wishlist = mapper.Map<Wishlist>(dto);
            await repository.AddAsync(wishlist);
            return CreatedAtAction(nameof(GetWishlist) , new { customerId = dto.CustomerId } , dto);
        }

        // DELETE: /wishlist/{customerId}/{productId}
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromWishlist(int customerId , int productId)
        {
            await repository.RemoveAsync(customerId , productId);
            return Ok("Item removed from wishlist.");
        }
    }
}
