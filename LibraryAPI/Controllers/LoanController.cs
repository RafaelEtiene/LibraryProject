using LibraryAPI.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        [HttpGet("GetBookLoanById/{idLoan}")]
        public async Task<IActionResult> GetBookLoanById([FromHeader] int idLoan)
        {
            return Ok();
        }

        [HttpPost("InsertNewBookLoan")]
        public async Task<IActionResult> InsertNewBookLoan([FromBody]LoanInfo request)
        {
            return Ok();
        }

        [HttpPost("RenovateLoan")]
        public async Task<IActionResult> RenovateLoan([FromBody]Client request)
        {
            return Ok();
        }

        [HttpPost("FinishLoan")]
        public async Task<IActionResult> FinishLoan([FromBody] int idLoan)
        {
            return Ok();
        }
    }
}
