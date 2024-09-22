using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Setup;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Request.Setup;
using Ecommerce.Models.ReturnDto.Setup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerManager _customerManager;

        public CustomerController(IMapper mapper, ICustomerManager customerManager)
        {
            _mapper = mapper;
            _customerManager = customerManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerCriteriaDto criteriaDto)
        {
            var result = await _customerManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var customerToReturn = _mapper.Map<List<CustomerReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var customer in customerToReturn)
                {
                    startCount = startCount + 1;
                    customer.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(customerToReturn);
            }
            return NoContent();
        }

        // Get a Customer by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerManager.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // Create a new Customer
        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            var result = await _customerManager.Add(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // Update a Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerCreateDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }
            var customer = _mapper.Map<Customer>(customerDto);
            var result = await _customerManager.Update(customer);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
