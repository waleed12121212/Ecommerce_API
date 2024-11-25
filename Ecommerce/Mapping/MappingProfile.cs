using AutoMapper;
using Ecommerce.Models;
using Ecommerce.DTOs.Category;
using Ecommerce.DTOs.Customer;
using Ecommerce.DTOs.Order;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.Wishlist;

namespace Ecommerce.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            // Customer
            CreateMap<Customer , CustomerDto>();
            CreateMap<CustomerCreateDto , Customer>();

            // Product
            CreateMap<Product , ProductDto>()
    .ForMember(dest => dest.CategoryName , opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductCreateDto , Product>();
            CreateMap<ProductUpdateDto , Product>();

            // Category
            CreateMap<Category , CategoryDto>();
            CreateMap<CategoryCreateDto , Category>();
            CreateMap<CategoryUpdateDto , Category>();

            // Order
            CreateMap<Order , OrderDto>()
                .ForMember(dest => dest.OrderDetails , opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderCreateDto , Order>()
                 .ForMember(dest => dest.OrderDetails , opt => opt.MapFrom(src => src.details));
            CreateMap<OrderDetails , OrderDetailsDto>();
            CreateMap<OrderDetailsCreateDto , OrderDetails>();

            // Wishlist
            CreateMap<Wishlist , WishlistDto>()
                .ForMember(dest => dest.ProductName , opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice , opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<WishlistCreateDto , Wishlist>();
        }
    }
}
