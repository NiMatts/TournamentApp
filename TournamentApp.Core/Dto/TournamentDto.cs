using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Dto
{
    public class TournamentDto
    {
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(3); // Slutdatum = startdatum + 3 månader

        public int Id { get; set; }
    }
}
