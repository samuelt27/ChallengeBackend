using System;
using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.Entities
{
    public record Movie
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime Release { get; init; }
        public double Rating { get; init; }
        public string Image { get; set; }
        public IList<MovieGenre> MovieGenres { get; init; } = new List<MovieGenre>();
        public IList<MovieCharacter> MovieCharacters { get; init; } = new List<MovieCharacter>();
    }
}
