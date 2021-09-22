using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.Entities
{
    public record Character
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Age { get; set; }
        public string Story { get; init; }
        public string Image { get; set; }
        public IList<MovieCharacter> MovieCharacters { get; init; } = new List<MovieCharacter>();
    }
}
