using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        // Foreign key to TournamentDetails
        public int TournamentId { get; set; }

        // Navigation property (optional but recommended)
        public Tournament Tournament { get; set; }
    }
}
