using Library.Application.Services.Interfaces;
using Library.Application.Exceptions;
using Library.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Library.Application.ViewModel;

namespace Library.API.Controllers
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
        public async Task<IActionResult> Token([FromBody]UserViewModel user)
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
