using Library.Application.Exceptions;
using Library.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Library.Application.Services.Interfaces;
using Library.Application.ViewModel;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookInfoViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var result = await _bookService.GetAllBooksAsync();

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when listing books. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("InsertBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertBook([FromBody]BookInfoViewModel request)
        {
            try
            {
                await _bookService.InsertBookAsync(request);

                return Ok();
            }
            catch (BusinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error (500) {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error ocurred when listing books. {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("InsertMassiveBooks")]
        public async Task<IActionResult> InsertMassiveBooks()
        {
            return Ok();
        }
    }
}
