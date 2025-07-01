using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentApp.Core.Entities;
using TournamentApp.Core.Repositories;
using TournamentApp.Data.Data;

namespace TournamnetApp.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentAppAPIContext _context;

        public GameRepository(TournamentAppAPIContext context)
        {
            _context = context;
        }

        public void Add(Game game)
        {
            _context.Game.Add(game);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Game.AnyAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.Include(g => g.Tournament).ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _context.Game
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
        }

        public void Update(Game game)
        {
            _context.Game.Update(game);
        }
    }
}
