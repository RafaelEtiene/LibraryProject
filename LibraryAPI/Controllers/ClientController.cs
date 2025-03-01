using Library.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize]
        [HttpGet("GetClients")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var result = await _clientService.GetClientsAsync();

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when listing clients. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("InsertNewClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertNewClient([FromBody]ClientViewModel request)
        {
            try
            {
                await _clientService.InsertNewClientAsync(request);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when insert client. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("GetClientById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientById([FromHeader]int idClient)
        {
            try
            {
                var result = await _clientService.GetClientByIdAsync(idClient);

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when get client by id {idClient}. {ex.Message}");
            }
        }
    }
}
