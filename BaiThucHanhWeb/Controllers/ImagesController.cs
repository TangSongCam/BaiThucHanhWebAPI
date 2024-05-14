using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.AspNetCore.Http;
using BaiThucHanhWeb.Repositories;
using System;
using System.IO;

namespace BaiThucHanhWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                // Convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription
                };

                // Use repository to upload image
                _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult GetAllInfoImage()
        {
            var allImage = _imageRepository.GetAllInfoImages();
            return Ok(allImage);
        }

        [HttpGet]
        [Route("download")]
        public IActionResult DownloadImage(int id)
        {
            var result = _imageRepository.DownloadFile(id);
            return File(result.Item1, result.Item2, result.Item3);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10400000)
            {
                ModelState.AddModelError("file", "File size too big, please upload file <10MB");
            }
        }
    }
}
