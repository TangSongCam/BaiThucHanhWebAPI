using Microsoft.AspNetCore.Mvc;
using BaiThucHanhWeb.Model.DTO;
using BaiThucHanhWeb.Repositories;
using BaiThucHanhWeb.Model.Domain;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsController(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        [HttpGet("get-all-authors")]
        [Authorize(Roles = "Read")]
        public IActionResult GetAll()
        {
            var allAuthors = _authorsRepository.GetAllAuthors();
            return Ok(allAuthors);
        }

        [HttpGet]
        [Route("get-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult GetAuthorById([FromRoute] int id)
        {
            var authorWithIdDTO = _authorsRepository.GetAuthorById(id);
            return Ok(authorWithIdDTO);
        }

        [HttpPost("add-author")]
        [Authorize(Roles = "Write")]
        public IActionResult AddAuthor([FromBody] AuthorsDTO authorDTO)
        {
            var addedAuthor = _authorsRepository.AddAuthor(authorDTO);
            return Ok(addedAuthor);
        }

        [HttpPut("update-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorsDTO authorDTO)
        {
            var author = new Authors
            {
                ID = id,
                FullName = authorDTO.FullName
            };

            var updateAuthor = _authorsRepository.UpdateAuthorById(id, author);
            return Ok(updateAuthor);
        }


        [HttpDelete("delete-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deletedAuthor = _authorsRepository.DeleteAuthorById(id);
            return Ok(deletedAuthor);
        }

        [HttpGet("get-all-authors-sorted-by-field")]
        [Authorize(Roles = "Write")]
        public IActionResult GetAllAuthorsSortedByField([FromQuery] string field, [FromQuery] bool ascending = true)
        {
            try
            {
                var allAuthorsSortedByField = _authorsRepository.GetAuthorsSortedByField(field, ascending);
                return Ok(allAuthorsSortedByField);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search-author")]
        [Authorize(Roles = "Write")]

        public IActionResult SearchAuthors([FromQuery] string keyword)
        {
            try
            {
                var foundAuthors = _authorsRepository.SearchAuthors(keyword);
                return Ok(foundAuthors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("get-authors-page")]
        [Authorize(Roles = "Write")]
        public IActionResult GetAuthorsPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var authorsPage = _authorsRepository.GetAuthorsPage(pageNumber, pageSize);
                return Ok(authorsPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
