namespace ChallengeBackend.WebAPI.DTOs.Characters
{
    public record GetCharactersDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Image { get; init; }
    }
}
