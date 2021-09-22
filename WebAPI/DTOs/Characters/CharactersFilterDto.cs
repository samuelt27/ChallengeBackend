namespace ChallengeBackend.WebAPI.DTOs.Characters
{
    public record CharactersFilterDto
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string Name { get; set; }
        public string Movie { get; set; }
    }
}
