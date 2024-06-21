using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NZWalk_API.Model.DTO.ImageDTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadRequestDto : ControllerBase
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
