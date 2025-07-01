using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Core.Repositories;
using TournamentApp.Data.Data;

namespace TournamnetApp.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TournamentAppAPIContext _context;
        public ITournamentRepository TournamentRepository { get; }

        public IGameRepository GameRepository { get; }

        public UoW(TournamentAppAPIContext context, ITournamentRepository tournamentRepo, IGameRepository gameRepo)
        {
            _context = context;
            TournamentRepository = tournamentRepo;
            GameRepository = gameRepo;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
