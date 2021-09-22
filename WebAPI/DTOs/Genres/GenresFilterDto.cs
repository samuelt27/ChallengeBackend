namespace ChallengeBackend.WebAPI.DTOs.Genres
{
    public record GenresFilterDto
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string Name { get; init; }
    }
}
