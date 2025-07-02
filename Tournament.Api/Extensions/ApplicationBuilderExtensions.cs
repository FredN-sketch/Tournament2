using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Service.Contracts;
using Tournament.Services;

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
        }
        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IGameService, GameService>();

        }
    }
}
