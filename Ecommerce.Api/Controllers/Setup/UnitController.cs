using AutoMapper;
using Ecommerce.Api.Common;
using Ecommerce.BLL.Abstraction.Setup;
using Ecommerce.BLL.Setup;
using Ecommerce.Database.Database;
using Ecommerce.Models.Common.Paging;
using Ecommerce.Models.CriteriaDto.Setup;
using Ecommerce.Models.Entities.Setup;
using Ecommerce.Models.ReturnDto.Setup;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitManager _unitManager;

        public UnitController(IMapper mapper, IUnitManager unitManager)
        {
            _mapper = mapper;
            _unitManager = unitManager;
        }
        [HttpGet()]
        public async Task<IActionResult> GetUnits([FromQuery] UnitCriteriaDto criteriaDto)
        {
            var units = await _unitManager.GetByCriteria(criteriaDto);
            if (units.Count() > 0)
            {
                var unitToReturn = _mapper.Map<IEnumerable<UnitReturnDto>>(units);
                var startCount = units.PageSize * (units.CurrentPage - 1);
                foreach (var unit in unitToReturn)
                {
                    startCount = startCount + 1;
                    unit.Sl = startCount;
                }

                 Response.AddPagination(units.CurrentPage, units.PageSize, units.TotalCount, units.TotalPages);
                return Ok(unitToReturn);
            }
            return NoContent();
        }

        // Get all Units
       

        // Get a Unit by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var unit = await _unitManager.GetById(id);

            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }

        // Create a new Unit
        [HttpPost]
        public async Task<IActionResult> Create(Unit unit)
        {
            var result  = await _unitManager.Add(unit);
            return CreatedAtAction(nameof(GetById), new { id = unit.Id }, unit);
        }

        // Update a Unit
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            var result = await _unitManager.Update(unit);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Unit
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
