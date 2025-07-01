using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentApp.Core.Dto
{
    public class TournamentUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tournament title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
    }
}
