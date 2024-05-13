using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using BaiThucHanhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersRepository _publishersRepository;

        public PublishersController(IPublishersRepository publishersRepository)
        {
            _publishersRepository = publishersRepository;
        }

        [HttpGet("get-all-publishers")]
        [Authorize(Roles = "Read")]
        public IActionResult GetAll()
        {
            var allPublishers = _publishersRepository.GetAllPublishers();
            return Ok(allPublishers);
        }

        [HttpGet]
        [Route("get-publishers-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult GetPublisherById([FromRoute] int id)
        {
            var publisher = _publishersRepository.GetPublisherById(id);
            return publisher != null ? Ok(publisher) : NotFound();
        }

        [HttpPost("add-publisher")]
        [Authorize(Roles = "Write")]
        public IActionResult AddPublisher([FromBody] PublishersDTO publisher)
        {
            var addedPublisher = _publishersRepository.AddPublisher(publisher);
            return CreatedAtAction(nameof(GetPublisherById), new { id = addedPublisher.ID }, addedPublisher);
        }

        [HttpPut("update-publisher-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdatePublisherById(int id, [FromBody] PublishersDTO publisherDTO)
        {
            var updatePublisher = _publishersRepository.UpdatePublisherById(id, publisherDTO);
            return Ok(updatePublisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeletePublisherById(int id)
        {
            var deletedPublisher = _publishersRepository.DeletePublisherById(id);
            return deletedPublisher != null ? Ok(deletedPublisher) : NotFound();
        }

        [HttpGet("get-all-publishers-sorted-by-field")]
        [Authorize(Roles = "Write")]
        public IActionResult GetAllPublishersSortedByField([FromQuery] string field, [FromQuery] bool ascending = true)
        {
            try
            {
                var allPublishersSortedByField = _publishersRepository.GetPublishersSortedByField(field, ascending);
                return Ok(allPublishersSortedByField);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("search-publishers")]
        [Authorize(Roles = "Write")]
        public IActionResult SearchPublishers([FromQuery] string keyword)
        {
            try
            {
                var foundPublishers = _publishersRepository.SearchPublishers(keyword);
                return Ok(foundPublishers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-publishers-page")]
        [Authorize(Roles = "Write")]
        public IActionResult GetPublishersPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var publishersPage = _publishersRepository.GetPublishersPage(pageNumber, pageSize);
                return Ok(publishersPage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
