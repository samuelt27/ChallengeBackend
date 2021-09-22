using ChallengeBackend.WebAPI.Identity.Interfaces;

namespace ChallengeBackend.WebAPI.Identity
{
    public record TokenParameters : ITokenParameters
    {
        public string Id { get; init; }
        public string UserName { get; init; }
        public string PasswordHash { get; init; }
    }
}
