using System;
using TournamentApp.Data.Data;
using TournamnetApp.Data.Data;

namespace TournamentApp.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TournamentAppAPIContext>();

            await SeedData.SeedAsync(context);
        }
    }
}
