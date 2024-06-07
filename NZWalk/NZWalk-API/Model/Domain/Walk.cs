namespace NZWalk_API.Model.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DiffucaltyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation Prop
        public Difficulty Diffucalty { get; set; }
        public Region Region { get; set; }






    }
}
