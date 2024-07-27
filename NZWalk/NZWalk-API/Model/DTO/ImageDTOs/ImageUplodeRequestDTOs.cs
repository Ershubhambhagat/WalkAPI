using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NZWalk_API.Model.DTO.ImageDTOs
{
    public class ImageUploadRequestDTOs
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string  FileName { get; set; }
        public string?  FileDiscription { get; set; }
    }
}
