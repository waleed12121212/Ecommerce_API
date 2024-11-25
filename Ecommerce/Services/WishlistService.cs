using AutoMapper;
using Ecommerce.DTOs.Wishlist;
using Ecommerce.Models;
using Ecommerce.Repositories;

namespace Ecommerce.Services
{
    public class WishlistService
    {
        private readonly IRepository<Wishlist> _repository;
        private readonly IMapper _mapper;

        public WishlistService(IRepository<Wishlist> repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WishlistDto>> GetWishlistByCustomerIdAsync(int customerId)
        {
            var wishlist = await _repository.GetAllAsync();
            var customerWishlist = wishlist.Where(w => w.CustomerId == customerId);
            return _mapper.Map<IEnumerable<WishlistDto>>(customerWishlist);
        }

        public async Task<bool> AddToWishlistAsync(WishlistCreateDto dto)
        {
            var wishlistItem = _mapper.Map<Wishlist>(dto);
            await _repository.AddAsync(wishlistItem);
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int customerId , int productId)
        {
            var wishlist = await _repository.GetAllAsync();
            var item = wishlist.FirstOrDefault(w => w.CustomerId == customerId && w.ProductId == productId);
            if (item == null) return false;

            await _repository.DeleteAsync(item.Id);
            return true;
        }
    }
}
