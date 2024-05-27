using Microsoft.EntityFrameworkCore;
using NZWalk_API.Model.DTO;

namespace NZWalk_API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
                
        }
        public DbSet<DifficultyDto> Difficulty { get; set; }
        public DbSet<RegionDto> Regions { get; set; }
        public DbSet<WalkDto> Walks { get; set; }

    }

}
