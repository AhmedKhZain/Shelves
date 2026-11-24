using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shelves.Services.BookUseCase;

namespace Shelves.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(string title, string description, int totalCopies, decimal reservationPricePerDay, Guid? authrId)
        {
            var result = await _bookService.AddBook(title, description, totalCopies, reservationPricePerDay, authrId);
            return MapResult(result);
        }


        [HttpGet("{id}")]   
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var result = await _bookService.GetBookById(id);
            return MapResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAll() ;
            return MapResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var result = await _bookService.DeleteBook(id);
            return MapResult(result);
        }

    }
}
