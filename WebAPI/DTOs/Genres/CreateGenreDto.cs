using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend.WebAPI.DTOs.Genres
{
    public record CreateGenreDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; init; }
    }
}
