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
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentAppAPIContext _context;

        public TournamentRepository(TournamentAppAPIContext context)
        {
            _context = context;
        }

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames = false)
        {
            return includeGames
                ? await _context.Tournament.Include(c => c.Games).ToListAsync()
                : await _context.Tournament.ToListAsync();
        }

        public async Task<Tournament> GetAsync(int id)
        {
            return await _context.Tournament
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Tournament.Update(tournament);
        }
    }
}
