namespace ChallengeBackend.WebAPI.DTOs.Movies
{
    public record MovieDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
    }
}
