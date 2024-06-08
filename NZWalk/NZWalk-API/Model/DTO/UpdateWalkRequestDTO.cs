using System.ComponentModel.DataAnnotations;

namespace NZWalk_API.Model.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        [Required]
        [Range(1, 100)]
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DiffucaltyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }


    }
}
