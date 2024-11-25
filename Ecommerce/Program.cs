using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Repositories.OrderRepository;
using Ecommerce.Repositories.ProductRepository;
using Ecommerce.Repositories.WishlistRepository;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped(typeof(IRepository<>) , typeof(Repository<>));
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<WishlistService>();

            builder.Services.AddScoped<IRepository<Product> , Repository<Product>>();
            builder.Services.AddScoped<IProductRepository , ProductRepository>();
            builder.Services.AddScoped<IOrderRepository , OrderRepository>();
            builder.Services.AddScoped<IWishlistRepository , WishlistRepository>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true ,
                        ValidateAudience = true ,
                        ValidateLifetime = true ,
                        ValidateIssuerSigningKey = true ,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"] ,
                        ValidAudience = builder.Configuration["Jwt:Audience"] ,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                }
            );

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll" , policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();


            app.UseCors("AllowAll");


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
