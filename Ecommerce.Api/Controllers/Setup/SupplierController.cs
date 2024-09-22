using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.Models.Common;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Request.Setup;
using Ecommerce.Models.ReturnDto.Setup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISupplierManager _supplierManager;

        public SupplierController(IMapper mapper, ISupplierManager supplierManager)
        {
            _mapper = mapper;
            _supplierManager = supplierManager;
        }

        // Get all Suppliers
        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] SupplierCriteriaDto criteriaDto)
        {
            var result = await _supplierManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var supplierToReturn = _mapper.Map<List<SupplierReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var supplier in supplierToReturn)
                {
                    startCount = startCount + 1;
                    supplier.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(supplierToReturn);
            }
            return NoContent();
        }

        // Get a Supplier by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierManager.GetById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // Create a new Supplier
        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            var result = await _supplierManager.Add(supplier);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }

        // Update a Supplier
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierCreateDto supplierDto)
        {
            if (id != supplierDto.Id)
            {
                return BadRequest();
            }
            var supplier = _mapper.Map<Supplier>(supplierDto);
            var result = await _supplierManager.Update(supplier);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Supplier
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _supplierManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
