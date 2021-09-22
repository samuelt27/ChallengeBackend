namespace ChallengeBackend.WebAPI.DTOs.Characters
{
    public record CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
    }
}
