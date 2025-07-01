using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Dto
{
    public record TournamentDto(DateTime StartDate)
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")]
        public string? Title { get; init; }
        public DateTime StartDate { get; init; } = StartDate;
        public DateTime EndDate { get; init; } = StartDate.AddMonths(3);
        public ICollection<GameDto>? Games { get; init; }
    }
}
