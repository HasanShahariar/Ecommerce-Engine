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
    public class BankController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBankManager _bankManager;

        public BankController(IMapper mapper, IBankManager bankManager)
        {
            _mapper = mapper;
            _bankManager = bankManager;
        }

        // Get all result
        [HttpGet]
        public async Task<IActionResult> GetBanks([FromQuery] BankCriteriaDto criteriaDto)
        {
            var result = await _bankManager.GetByCriteria(criteriaDto);
            if (result.Count() > 0)
            {
                var unitToReturn = _mapper.Map<IEnumerable<BankReturnDto>>(result);
                var startCount = result.PageSize * (result.CurrentPage - 1);
                foreach (var unit in unitToReturn)
                {
                    startCount = startCount + 1;
                    unit.Sl = startCount;
                }

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                return Ok(unitToReturn);
            }
            return NoContent();
        }

        // Get a Bank by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bank = await _bankManager.GetById(id);

            if (bank == null)
            {
                return NotFound();
            }

            return Ok(bank);
        }

        // Create a new Bank
        [HttpPost]
        public async Task<IActionResult> Create(BankCreateDto bankDto)
        {
            try
            {
                var bank = _mapper.Map<Bank>(bankDto);
                var result = await _bankManager.Add(bank);
                return CreatedAtAction(nameof(GetById), new { id = bank.Id }, bank);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Update a Bank
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BankCreateDto bankDto)
        {
            if (id != bankDto.Id)
            {
                return BadRequest();
            }
            var bank = _mapper.Map<Bank>(bankDto);

            var result = await _bankManager.Update(bank);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Conflict(result.Errors[0]);

        }

        // Delete a Bank
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bankManager.Remove(id);

            if (result.Succeeded)
                return Ok();
            return Conflict(result.Errors[0]);
        }
    }
}
