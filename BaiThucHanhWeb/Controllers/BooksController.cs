using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.OpenApi.Models;
using BaiThucHanhWeb.Data;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public BooksController(BookDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooksDomain = _dbContext.Books;

            var allBooksDTO = allBooksDomain.Select(book => new BookDTO()
            {
                ID = book.ID,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead ?? null,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorName = book.BookAuthors.Select(author => author.Author.FullName).ToList()
            }).ToList();


            return Ok(allBooksDTO);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = new BookDTO()
            {
                ID = book.ID,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead ?? null,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorName = book.BookAuthors.Select(author => author.Author.FullName).ToList()
            };

            return Ok(bookDTO);
        }
    }
}
