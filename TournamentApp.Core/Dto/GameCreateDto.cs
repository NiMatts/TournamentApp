using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Dto
{
    public class GameCreateDto
    {
        [Required(ErrorMessage = "Game title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Title { get; set; } = null!;
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
    }
}
