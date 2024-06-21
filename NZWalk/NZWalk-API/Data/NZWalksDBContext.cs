using Microsoft.EntityFrameworkCore;
using NZWalk_API.Model.Domain;
using NZWalk_API.Model.DTO;

namespace NZWalk_API.Data
{
    public class NZWalksDBContext:DbContext
    {
        //Type implemented
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions):base(dbContextOptions)
        {
                
        }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<RegionDto> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region //Seed Data for Difficulties
         //   var difficulties = new List<Difficulty>()
            //{
            //    new Difficulty()
            //    {
            //        Id=Guid.Parse("dd1fde0e-6a4f-4ecc-87ef-32302c63aa29"),
            //        Name="Easy"
            //    },
            //    new Difficulty()
            //    {
            //        Id=Guid.Parse("6527876e-ef1c-4085-95c4-342750a6a3cd"),
            //        Name="Medium"
            //    },new Difficulty()
            //    {
            //        Id=Guid.Parse("78e1a2d7-57da-4719-8c89-2eada106de7e"),
            //        Name="Hard"
            //    },

            //};
            ////Seed Difficulties in DataBase
           // modelBuilder.Entity<Difficulty>().HasData(difficulties);

            #endregion


            #region //Seed Data For Region


            var regions = new List<Region>()
            {
                  new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
            #endregion
        }

    }

}
