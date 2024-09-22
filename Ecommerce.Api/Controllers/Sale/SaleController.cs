using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Common;
using Ecommerce.BLL.Abstraction.Sale;
using Ecommerce.Models.CriteriaDto.Sale;
using Ecommerce.Models.Entities.Sale;
using Ecommerce.Models.Request.Sale;
using Ecommerce.Models.ReturnDto.Sale;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Sale
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISaleManager _salesManager;
        private readonly ICodeGenerationService _codeGenerationService;



        public SaleController(IMapper mapper, ISaleManager salesManager, ICodeGenerationService codeGenerationService)
        {
            _mapper = mapper;
            _salesManager = salesManager;
            _codeGenerationService = codeGenerationService;
        }

        // Get all Sales
        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery] SaleCriteriaDto criteriaDto)
        {
            var result = await _salesManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var saleToReturn = _mapper.Map<List<SaleReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var sale in saleToReturn)
                {
                    startCount = startCount + 1;
                    sale.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(saleToReturn);
            }
            return NoContent();
        }

        // Get a Sale by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _salesManager.GetById(id);

            var sales = _mapper.Map<SaleReturnDto>(result);


            if (sales == null)
            {
                return NotFound();
            }

            return Ok(sales);
        }

        // Create a new Sales
        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateDto salesDto)
        {
            try
            {
                var sales = _mapper.Map<Sales>(salesDto);
                var result = await _salesManager.AddAsync(sales);
                return CreatedAtAction(nameof(GetById), new { id = sales.Id }, sales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update a Sales
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SaleCreateDto salesDto)
        {
            if (id != salesDto.Id)
            {
                return BadRequest();
            }
            var sales = _mapper.Map<Sales>(salesDto);

            var result = await _salesManager.Update(sales);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors);

        }

        // Delete a Sales
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _salesManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
