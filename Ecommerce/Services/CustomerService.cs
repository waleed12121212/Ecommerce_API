using AutoMapper;
using Ecommerce.DTOs.Customer;
using Ecommerce.Models;
using Ecommerce.Repositories;

namespace Ecommerce.Services
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync( )
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> CreateCustomerAsync(CustomerCreateDto dto)
        {
            var existingCustomer = await _repository.GetAllAsync();
            if (existingCustomer.Any(c => c.Email == dto.Email)) return false;

            var customer = _mapper.Map<Customer>(dto);
            await _repository.AddAsync(customer);
            return true;
        }
    }
}
