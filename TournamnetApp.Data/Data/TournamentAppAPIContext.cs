using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentApp.Core.Entities;

namespace TournamentApp.Data.Data
{
    public class TournamentAppAPIContext : DbContext
    {
        public TournamentAppAPIContext (DbContextOptions<TournamentAppAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentApp.Core.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<TournamentApp.Core.Entities.Game> Game { get; set; } = default!;
    }
}
