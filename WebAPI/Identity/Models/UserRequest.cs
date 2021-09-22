using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend.WebAPI.Identity.Models
{
    public record UserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}
