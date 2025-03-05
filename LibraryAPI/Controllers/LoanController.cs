using Library.Application.Exceptions;
using Library.Core.Entities;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Library.Application.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpGet("GetAllLoans")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LoanInfo>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                var result = await _loanService.GetAllLoans();
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest($"An error ocurred in get loan by id. {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred in get loan by id. {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("GetBookLoanById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoanInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBookLoanById([FromHeader] int idLoan)
        {
            try
            {
                var result = await _loanService.GetBookLoanByIdAsync(idLoan);
                return Ok(result);
            }
            catch(BusinessException ex)
            {
                return BadRequest($"An error ocurred in get loan by id. {ex.Message}");
            }
            catch(Exception ex)
            {
                return BadRequest($"An error ocurred in get loan by id. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("InsertNewBookLoan")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertNewBookLoan([FromBody]LoanInsertDTO request)
        {
            try
            {
                var result = await _loanService.InsertNewBookLoanAsync(request);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest($"An error ocurred in insert loan. {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred in insert loan. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("RenovateLoan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RenovateLoan([FromBody]int idLoan)
        {
            try
            {
                await _loanService.RenovateLoanAsync(idLoan);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest($"An error ocurred in renovate loan. {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred in renovate loan. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("FinishLoan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinishLoan([FromBody] int idLoan)
        {
            try
            {
                await _loanService.FinishLoanAsync(idLoan);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest($"An error ocurred in finish loan. {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred in finish loan. {ex.Message}");
            }
        }
    }
}
