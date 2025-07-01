using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }

        // Navigation property (1 tournament → many games)
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
