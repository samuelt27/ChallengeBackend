using ChallengeBackend.WebAPI.DTOs.Characters;
using ChallengeBackend.WebAPI.DTOs.Genres;
using System;
using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.DTOs.Movies
{
    public record GetMovieDetailsDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime Release { get; init; }
        public double Rating { get; init; }
        public string Image { get; set; }
        public List<GenreDto> Genres { get; init; }
        public List<CharacterDto> Characters { get; init; }
    }
}
