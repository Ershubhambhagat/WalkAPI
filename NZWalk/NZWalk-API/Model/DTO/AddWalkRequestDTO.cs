namespace NZWalk_API.Model.DTO
{
    public class AddWalkRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        public Guid DiffucaltyId { get; set; }

        public Guid RegionId { get; set; }
    }
}
