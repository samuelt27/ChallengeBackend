using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.Entities
{
    public record Genre
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IList<MovieGenre> MovieGenres { get; init; } = new List<MovieGenre>();
    }
}
