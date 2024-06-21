using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO.ImageDTOs;
using NZWalk_API.Repositories.Image;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        #region CTOR
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        #endregion

        #region UplodeImage
        [HttpPost]
        public async Task<IActionResult> Uplode([FromForm] ImageUploadRequestDto request)
        {

            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //DTOs to Domain Model 
                var imgaeDomainModel = new Image
                {
                    File = request.File,
                    //get File Extension
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,
                };

                //Uploading Image using repo 
                await _imageRepository.Upload(imgaeDomainModel);
                return Ok(imgaeDomainModel);

            }
            return BadRequest(ModelState);
        }


        #endregion


        #region ValidateFileUpload
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[]
            {
               " .jpg",".png"
            };
            //If File Extension not belon to this Then
            if (!allowedExtension.Contains(Path.GetExtension(request.FileName)))
            {
                //Give error 
                ModelState.AddModelError("File", "Unsupported File Extension");

            }
            // if File is more then 10 MB then
            if (request.File.Length > 10485760)
            {
                //Give error 
                ModelState.AddModelError("File", "File Size More then 10 MB, Uplode Smaller File");
            }
        }



        #endregion


    }
}
