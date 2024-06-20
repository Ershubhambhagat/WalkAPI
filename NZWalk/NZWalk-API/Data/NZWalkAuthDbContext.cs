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
            var readerRoleID = "e7b1286e-ad2e-45da-8130-a033d302ab65";
            var writerRoleID = "8abb16f0-da36-487d-8505-5585700fddf9";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id="readerRoleID",
                    ConcurrencyStamp=readerRoleID,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id="writerRoleID",
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
