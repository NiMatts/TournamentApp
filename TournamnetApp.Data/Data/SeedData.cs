using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Core.Entities;
using TournamentApp.Data.Data;

namespace TournamnetApp.Data.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(TournamentAppAPIContext context)
        {
            if (!context.Tournament.Any())
            {
                var tournament = new Tournament
                {
                    Title = "Autumn Invitational",
                    StartDate = DateTime.Parse("2024-09-10"),
                    Games = new List<Game>
                    {
                        new Game { Title = "Match 1", Time = DateTime.Now.AddHours(1) },
                        new Game { Title = "Match 2", Time = DateTime.Now.AddHours(3) },
                    }
                };

                var tournament2 = new Tournament
                {
                    Title = "Spring Championship",
                    StartDate = DateTime.Parse("2025-03-21"),
                    Games = new List<Game>
                    {
                        new Game { Title = "Opening Match", Time = DateTime.Now.AddDays(1) },
                        new Game { Title = "Final", Time = DateTime.Now.AddDays(2) }
                    }
                };

                context.Tournament.AddRange(tournament, tournament2);
                await context.SaveChangesAsync();
            }
        }
    }
}
