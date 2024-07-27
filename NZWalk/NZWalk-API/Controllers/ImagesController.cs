using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.DTO.ImageDTOs;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Uplode")]
        public async Task<IActionResult> upload([FromForm]ImageUplodeRequestDTOs requestImage)
        {
            ValidateFileUpload(requestImage);
            if(ModelState.IsValid)
            {
                //uplode file path in database

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
