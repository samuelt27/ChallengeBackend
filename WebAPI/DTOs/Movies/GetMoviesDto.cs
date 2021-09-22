using System;

namespace ChallengeBackend.WebAPI.DTOs.Movies
{
    public record GetMoviesDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime Release { get; init; }
        public string Image { get; init; }
    }
}
