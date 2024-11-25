using AutoMapper;
using Ecommerce.DTOs.Customer;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> repository;
        private readonly IMapper mapper;

        public CustomerController(IRepository<Customer> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCustomers( )
        {
            var customers = await repository.GetAllAsync();
            var customerDtos = mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            var customerDto = mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDto dto)
        {
            var customer = mapper.Map<Customer>(dto);
            await repository.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById) , new { id = customer.Id } , mapper.Map<CustomerDto>(customer));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer(int id , CustomerCreateDto dto)
        {
            var existingCustomer = await repository.GetByIdAsync(id);
            if (existingCustomer == null) return NotFound();

            mapper.Map(dto , existingCustomer);
            await repository.UpdateAsync(existingCustomer);
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await repository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
