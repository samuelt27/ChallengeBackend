namespace ChallengeBackend.WebAPI.Identity
{
    public record Jwt
    {
        public string Secret { get; init; }
    }
}
