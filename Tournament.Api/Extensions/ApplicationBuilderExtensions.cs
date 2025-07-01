using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;

namespace Tournament.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<TournamentApiContext>();
                await db.Database.MigrateAsync();
                if (await db.TournamentDetails.AnyAsync())
                {
                    return; // Database has been seeded
                }

                try
                {
                    var tournaments = SeedData.GenerateTournaments();
                    //SeedData.GenerateTournaments();
                    db.AddRange(tournaments);
                    await db.SaveChangesAsync();
                }
                catch (Exception) 
                { 
                    throw; 
                }

               
            }

            //static void GenerateTournaments()
            //{
            //    var tournaments = new List<TournamentDetail>
            //        {
            //            new TournamentDetail
            //            {
            //                Name = "Spring Championship",
            //                Location = "New York",
            //                StartDate = DateTime.UtcNow.AddDays(30),
            //                EndDate = DateTime.UtcNow.AddDays(35),
            //                Teams = new List<Team>
            //                {
            //                    new Team { Name = "Team A" },
            //                    new Team { Name = "Team B" }
            //                }
            //            },
            //            new TournamentDetail
            //            {
            //                Name = "Summer Showdown",
            //                Location = "Los Angeles",
            //                StartDate = DateTime.UtcNow.AddDays(60),
            //                EndDate = DateTime.UtcNow.AddDays(65),
            //                Teams = new List<Team>
            //                {
            //                    new Team { Name = "Team C" },
            //                    new Team { Name = "Team D" }
            //                }
            //            }
            //        };
            //}
        }
    }
}
