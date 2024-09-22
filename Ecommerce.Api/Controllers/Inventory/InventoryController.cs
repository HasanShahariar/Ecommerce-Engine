using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Common;
using Ecommerce.BLL.Abstraction.Inventory;
using Ecommerce.BLL.Abstraction.Purchase;
using Ecommerce.Models.CriteriaDto.Inventory;
using Ecommerce.Models.CriteriaDto.Purchase;
using Ecommerce.Models.ReturnDto.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryManager _inventoryManager;
        public InventoryController(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory([FromQuery] InventoryCriteriaDto criteriaDto)
        {
            var result = await _inventoryManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var invetory in result)
                {
                    startCount = startCount + 1;
                    invetory.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(result);
            }
            return NoContent();
        }
    }
}
