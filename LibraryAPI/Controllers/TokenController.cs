using Library.Data.Services.Interfaces;
using Library.Model.Exceptions;
using Library.Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TokenController(IAuthService authService)
        {
            _authService = authService;
        }   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Token([FromBody]Users user)
        {
            try
            {
                var token = await _authService.GenerateJwtToken(user);
                return Ok(token);
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");

            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when get token. {ex.Message}");

            }
        }
    }
}
