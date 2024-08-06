using Library.Model.Exceptions;
using Library.Model.Model;
using Library.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
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

        [HttpGet("GetAllBooks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookInfo>))]
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

        [HttpPost("InsertBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertBook([FromBody]BookInfo request)
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

        [HttpPost("InsertMassiveBooks")]
        public async Task<IActionResult> InsertMassiveBooks()
        {
            return Ok();
        }
    }
}
