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
    public class ItemTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IItemTypeManager _itemTypeManager;

        public ItemTypeController(IMapper mapper, IItemTypeManager itemTypeManager)
        {
            _mapper = mapper;
            _itemTypeManager = itemTypeManager;
        }

        // Get all ItemTypes
        [HttpGet]
        public async Task<IActionResult> GetItemTypes([FromQuery] ItemTypeCriteriaDto criteriaDto)
        {
            var result = await _itemTypeManager.GetByCriteria(criteriaDto);

            if (result.Count() > 0)
            {
                var itemTypeToReturn = _mapper.Map<List<ItemTypeReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var itemType in itemTypeToReturn)
                {
                    startCount = startCount + 1;
                    itemType.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(itemTypeToReturn);
            }
            return NoContent();
        }

        // Get a ItemType by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var itemType = await _itemTypeManager.GetById(id);

            if (itemType == null)
            {
                return NotFound();
            }

            return Ok(itemType);
        }

        // Create a new ItemType
        [HttpPost]
        public async Task<IActionResult> Create(ItemTypeCreateDto itemTypeDto)
        {
            var itemType = _mapper.Map<ItemType>(itemTypeDto);
            var result = await _itemTypeManager.Add(itemType);
            return CreatedAtAction(nameof(GetById), new { id = itemType.Id }, itemType);
        }

        // Update a ItemType
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemTypeCreateDto itemTypeDto)
        {
            if (id != itemTypeDto.Id)
            {
                return BadRequest();
            }
            var itemType = _mapper.Map<ItemType>(itemTypeDto);

            var result = await _itemTypeManager.Update(itemType);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a ItemType
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemTypeManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
