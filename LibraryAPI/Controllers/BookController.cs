using LibraryAPI.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok();
        }

        [HttpPost("InsertBook")]
        public async Task<IActionResult> InsertBook(BookInfo request)
        {
            return Ok();
        }

        [HttpPost("InsertMassiveBooks")]
        public async Task<IActionResult> InsertMassiveBooks()
        {
            return Ok();
        }
    }
}
