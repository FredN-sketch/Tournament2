using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")]
        public string? Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentDetailsId { get; set; }
        // Navigation property to TournamentDetails
        public TournamentDetails? TournamentDetails { get; set; }
    }
}
