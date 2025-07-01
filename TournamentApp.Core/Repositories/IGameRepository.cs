using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Core.Entities;

namespace TournamentApp.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Game tournament);
        void Update(Game tournament);
        void Remove(Game tournament);
    }
}
