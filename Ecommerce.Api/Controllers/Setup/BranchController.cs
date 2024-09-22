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
    public class BranchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBranchManager _branchManager;

        public BranchController(IMapper mapper, IBranchManager branchManager)
        {
            _mapper = mapper;
            _branchManager = branchManager;
        }

        // Get all Branchs
        [HttpGet]
        public async Task<IActionResult> GetBranchs([FromQuery] BranchCriteriaDto criteriaDto)
        {
            var result = await _branchManager.GetByCriteria(criteriaDto);
       

            if (result.Count() > 0)
            {
                var branchesToReturn = _mapper.Map<List<BranchReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var unit in branchesToReturn)
                {
                    startCount = startCount + 1;
                    unit.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(branchesToReturn);
            }
            return NoContent();
        }

        // Get a Branch by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _branchManager.GetById(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        // Create a new Branch
        [HttpPost]
        public async Task<IActionResult> Create(BranchCreateDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto);
            var result = await _branchManager.Add(branch);
            return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
        }

        // Update a Branch
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BranchCreateDto branchDto)
        {
            if (id != branchDto.Id)
            {
                return BadRequest();
            }
            var branch = _mapper.Map<Branch>(branchDto);

            var result = await _branchManager.Update(branch);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Branch
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _branchManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
