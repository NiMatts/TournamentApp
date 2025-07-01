using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Dto
{
    public class GameDto
    {
        public string Title { get; set; } = null!;
        public DateTime Time { get; set; } // Startdatum för matchen
    }
}
