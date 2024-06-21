using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk_API.Model.DTO.ImageDTOs;

namespace NZWalk_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {


        #region Uplode
        [HttpPost]
        public async Task<IActionResult> Uplode([FromForm] ImageUploadRequestDto request)
        {

            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //Uplodeing Image 
                return Ok();


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


        #region MyRegion

        #endregion


        #region MyRegion

        #endregion
    }
}
