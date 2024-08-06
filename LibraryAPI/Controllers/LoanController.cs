using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Library.Model.DTO;

namespace LibraryAPI.Controllers
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
