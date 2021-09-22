using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.Identity.Models
{
    public record LoginResponse
    {
        public string Token { get; init; }
        public bool Login { get; init; }
        public IList<string> Errors { get; init; }
    }
}
