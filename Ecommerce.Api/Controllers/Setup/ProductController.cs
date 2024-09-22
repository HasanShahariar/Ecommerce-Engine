using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Request.Setup;
using Ecommerce.Models.ReturnDto.Setup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductManager _productManager;

        public ProductController(IMapper mapper, IProductManager productManager)
        {
            _mapper = mapper;
            _productManager = productManager;
        }

        // Get all Products
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductCriteriaDto criteriaDto)
        {
            var result = await _productManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var productsToReturn = _mapper.Map<List<ProductReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var product in productsToReturn)
                {
                    startCount = startCount + 1;
                    product.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(productsToReturn);
            }
            return NoContent();
        }
        [HttpGet("GetAllFromVW_Product")]
        public async Task<IActionResult> GetAllFromVW_Product([FromQuery] ProductCriteriaDto criteriaDto)
        {
            var result = await _productManager.GetAllFromVW_Product(criteriaDto);
            

            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NoContent();
        }
        

        // Get a Product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productManager.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // Create a new Product
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Code = "1";
            var result = await _productManager.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // Update a Product
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);

            var result = await _productManager.Update(product);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
