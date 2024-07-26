using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalk_API.Data
{
    public class NZWalkAuthDbContext : IdentityDbContext
    {
        public NZWalkAuthDbContext(DbContextOptions<NZWalkAuthDbContext> options) : base(options)
        {
        }
        //write override and OnModelCreating and tab - tab
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleID = "d859333c-eea5-48d6-ad98-9c968fb75491";
            var writerRoleID = "2aaf722c-d5b9-4a53-a55f-b5f4eaa130eb";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleID,
                    ConcurrencyStamp=readerRoleID,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerRoleID,
                    ConcurrencyStamp=writerRoleID,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                },
            };



            //If Role is not available in database then it Seed In database.
            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
