using System.ComponentModel.DataAnnotations;

namespace NZWalk_API.Model.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be minimum 3 Character")]
        [MaxLength(30, ErrorMessage = "Name has to be Maximum 30 Character")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum 3 Character")]
        [MaxLength(4, ErrorMessage = "Code has to be Maximum 4 Character")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
