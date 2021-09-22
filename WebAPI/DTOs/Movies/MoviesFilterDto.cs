namespace ChallengeBackend.WebAPI.DTOs.Movies
{
    public record MoviesFilterDto
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string Title { get; init; }
        public string Genre { get; init; }
        public string Order { get; set; }
    }
}
