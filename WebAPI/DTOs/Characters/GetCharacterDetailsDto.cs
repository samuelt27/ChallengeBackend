using ChallengeBackend.WebAPI.DTOs.Movies;
using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.DTOs.Characters
{
    public class GetCharacterDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Age { get; init; }
        public string Story { get; init; }
        public string Image { get; init; }
        public List<MovieDto> Movies { get; init; }
    }
}
