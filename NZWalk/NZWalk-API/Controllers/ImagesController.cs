using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO.ImageDTOs;
using NZWalk_API.Repositories.Image_Repository;

namespace NZWalk_API.Controllers
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
        [Route("Uplode")]
        public async Task<IActionResult> upload([FromForm]ImageUplodeRequestDTOs requestImage)
        {


           
            ValidateFileUpload(requestImage);
            if(ModelState.IsValid)
            {
                // convet DTO to domain Model

                var imageDomainModel = new Image
                {
                    File = requestImage.File,
                    FileExtension = Path.GetExtension(requestImage.File.FileName),
                    FileDescription = requestImage.FileDiscription,
                    FileSizeInBytes = requestImage.File.Length,
                    FileName = requestImage.FileName
                };


                //uplode file path in database
                await _imageRepository.Uplode(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);

        }
        //validate image Extension File and size
        private void ValidateFileUpload(ImageUplodeRequestDTOs requestImage)
        {
            var allowExtension = new string[] { ".jpg",".png"};
            if (!allowExtension.Contains(Path.GetExtension(requestImage.File.FileName)))
            {
                ModelState.AddModelError("File", "FileExtension is not correct , use only JPG and Png file");
            }
            if(requestImage.File.Length > 1000000)
            {
                ModelState.AddModelError("File", "File Size is more then 1 MB");


            }
        }
    }
}
