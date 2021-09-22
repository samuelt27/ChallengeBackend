namespace ChallengeBackend.WebAPI.Entities
{
    public record MovieCharacter
    {
        public int MovieId { get; init; }
        public Movie Movie { get; init; }
        public int CharacterId { get; init; }
        public Character Character { get; init; }
    }
}
