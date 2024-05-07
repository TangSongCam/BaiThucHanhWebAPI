using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.AspNetCore.Http;
using BaiThucHanhWeb.Migrations;
using BaiThucHanhWeb.Repositories;
using System;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;
        private readonly IBookRepository _bookRepository;

        public BooksController(BookDbContext dbContext, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            // su dung reposity pattern  
            var allBooks = _bookRepository.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
        }
        [HttpPost("add - book")]
        public IActionResult AddBook([FromBody] AdBookRequestDTO adBookRequestDTO)
        {
            var bookAdd = _bookRepository.AddBook(adBookRequestDTO);
            return Ok(bookAdd);
        }

        /*[HttpPost("add-book")]
        [ValidateModel]
        public IActionResult AddBook([FromBody] AdBookRequestDTO adBookRequestDTO)
        {
            if (!ValidateAddBook(adBookRequestDTO))
            {
                return BadRequest(ModelState);
            }
            // before add da

            if (ModelState.IsValid)
            {
                var bookAdd = _bookRepository.AddBook(adBookRequestDTO);
                return Ok(bookAdd);
            }
            else return BadRequest(ModelState);
        }*/


        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AdBookRequestDTO bookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            return Ok(deleteBook);
        }

        [HttpGet("get-all-books-sorted-by-field")]
        public IActionResult GetAllBooksSortedByField([FromQuery] string field, [FromQuery] bool ascending = true)
        {
            try
            {
                var allBooksSortedByField = _bookRepository.GetBooksSortedByField(field, ascending);
                return Ok(allBooksSortedByField);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search-books")]
        public IActionResult SearchBooks([FromQuery] string keyword)
        {
            try
            {
                var foundBooks = _bookRepository.SearchBooks(keyword);
                return Ok(foundBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("get-books-page")]
        public IActionResult GetBooksPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var booksPage = _bookRepository.GetBooksPage(pageNumber, pageSize);
                return Ok(booksPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /*#region Private methods 
        private bool ValidateAddBook(AdBookRequestDTO adBookRequestDTO)
        {
            if (adBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(adBookRequestDTO), $"Please add book data");
                return false;
            }
            // kiem tra Description NotNull 
            if (string.IsNullOrEmpty(adBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(adBookRequestDTO.Description), $"{nameof(adBookRequestDTO.Description)} cannot be null");
            }
            // kiem tra rating (0,5) 
            if (adBookRequestDTO.Rate < 0 || adBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(adBookRequestDTO.Rate), $"{nameof(adBookRequestDTO.Rate)} cannot be less than 0 and more than 5");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion*/
    }
}
