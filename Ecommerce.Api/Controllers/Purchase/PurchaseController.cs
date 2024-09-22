using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Base;
using Ecommerce.BLL.Abstraction.Common;
using Ecommerce.BLL.Abstraction.Purchase;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Base;
using Ecommerce.BLL.Common;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Purchase;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.Request.Purchase;
using Ecommerce.Models.Request.Setup;
using Ecommerce.Models.ReturnDto.Purchase;
using Ecommerce.Models.ReturnDto.Setup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Purchase
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseManager _purchasesManager;
        private readonly ICodeGenerationService _codeGenerationService;
    


        public PurchaseController(IMapper mapper, IPurchaseManager purchasesManager, ICodeGenerationService codeGenerationService)
        {
            _mapper = mapper;
            _purchasesManager = purchasesManager;
            _codeGenerationService = codeGenerationService;
        }

        // Get all Purchasess
        [HttpGet]
        public async Task<IActionResult> GetPurchasess([FromQuery] PurchaseCriteriaDto criteriaDto)
        {
            var result = await _purchasesManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var purchaseToReturn = _mapper.Map<List<PurchaseReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var purchase in purchaseToReturn)
                {
                    startCount = startCount + 1;
                    purchase.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(purchaseToReturn);
            }
            return NoContent();
        }

        // Get a Purchases by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _purchasesManager.GetById(id);

            var purchases = _mapper.Map<PurchaseReturnDto>(result);


            if (purchases == null)
            {
                return NotFound();
            }

            return Ok(purchases);
        }

        // Create a new Purchases
        [HttpPost]
        public async Task<IActionResult> Create(PurchaseCreateDto purchasesDto)
        {
            try
            {
                var purchases = _mapper.Map<Purchases>(purchasesDto);
                var result = await _purchasesManager.AddAsync(purchases);
                return CreatedAtAction(nameof(GetById), new { id = purchases.Id }, purchases);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update a Purchases
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PurchaseCreateDto purchasesDto)
        {
            if (id != purchasesDto.Id)
            {
                return BadRequest();
            }
            var purchases = _mapper.Map<Purchases>(purchasesDto);

            var result = await _purchasesManager.Update(purchases);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors);

        }

        // Delete a Purchases
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _purchasesManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
