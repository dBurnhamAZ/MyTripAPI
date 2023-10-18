using Microsoft.EntityFrameworkCore;
using MyTripAPI.Model;

namespace MyTripAPI.Data
{
    public class MyTripAPIDbContext : DbContext
    {
        public MyTripAPIDbContext(DbContextOptions<MyTripAPIDbContext> options)
                : base(options)
        {
        }

        public DbSet<Suite> Suites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suite>().HasData(
                new Suite()
                {
                    Id = 1,
                    Name = "Royal Suite",
                    Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                    Rate = 220,
                    Occupancy = 5,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now

                },
                 new Suite()
                 {
                     Id = 2,
                     Name = "Ghoyal Suite",
                     Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                     ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                     Rate = 120,
                     Occupancy = 4,
                     Sqft = 230,
                     Amenity = "",
                     CreatedDate = DateTime.Now

                 },
                  new Suite()
                  {
                      Id = 3,
                      Name = "Shriya Ghoshal Suite",
                      Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                      ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                      Rate = 320,
                      Occupancy = 3,
                      Sqft = 140,
                      Amenity = "",
                      CreatedDate = DateTime.Now

                  },
                   new Suite()
                   {
                       Id = 4,
                       Name = "Social Suite",
                       Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                       ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                       Rate = 310,
                       Occupancy = 2,
                       Sqft = 400,
                       Amenity = "",
                       CreatedDate = DateTime.Now

                   },
                    new Suite()
                    {
                        Id = 5,
                        Name = "Ghost Suite",
                        Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                        Rate = 780,
                        Occupancy = 1,
                        Sqft = 480,
                        Amenity = "",
                        CreatedDate = DateTime.Now
                    },
                     new Suite()
                     {
                         Id = 6,
                         Name = "Koyal Ka Ghosla Suite",
                         Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                         ImageUrl = "C:\\Users\\nerme\\full-stack-learning-projects\\DotNetMastery_RESTAPIS\\ASP.NET MVC\\MagicSuite\\MagicSuite_SuiteAPI\\Images\\01.jpg",
                         Rate = 447,
                         Occupancy = 6,
                         Sqft = 700,
                         Amenity = "",
                         CreatedDate = DateTime.Now
                     }
                );
        }
    }
}
