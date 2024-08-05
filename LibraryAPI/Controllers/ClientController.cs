using LibraryAPI.Domain.Exceptions;
using LibraryAPI.Domain.Model;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
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

        [HttpGet("GetClients")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Client>))]
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

        [HttpPost("InsertNewClient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertNewClient([FromBody]Client request)
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

        [HttpPost("GetClientById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
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
