using AutoMapper;
using ChallengeBackend.WebAPI.DTOs.Characters;
using ChallengeBackend.WebAPI.DTOs.Genres;
using ChallengeBackend.WebAPI.DTOs.Movies;
using ChallengeBackend.WebAPI.Entities;
using System.Collections.Generic;

namespace ChallengeBackend.WebAPI.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Genres
            CreateMap<CreateGenreDto, Genre>();

            CreateMap<Genre, GenreDto>();


            // Movies
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(movie => movie.Image, options => options.Ignore())
                .ForMember(movie => movie.MovieGenres, options => options.MapFrom(MovieGenresMapping))
                .ForMember(movie => movie.MovieCharacters, options => options.MapFrom(MovieCharactersMapping));

            CreateMap<Movie, GetMoviesDto>();

            CreateMap<Movie, GetMovieDetailsDto>()
                .ForMember(movieDto => movieDto.Genres, options => options.MapFrom(GenresMapping))
                .ForMember(movieDto => movieDto.Characters, options => options.MapFrom(CharactersMapping));


            // Characters
            CreateMap<CreateCharacterDto, Character>()
                .ForMember(character => character.Image, options => options.Ignore());

            CreateMap<Character, GetCharactersDto>();

            CreateMap<Character, GetCharacterDetailsDto>()
                .ForMember(characterDto => characterDto.Movies, options => options.MapFrom(MoviesMapping));
        }


        #region MovieGenres
        private IList<MovieGenre> MovieGenresMapping(CreateMovieDto dto, Movie movie)
        {
            var result = new List<MovieGenre>();

            if (dto.Genres is null)
                return result;

            foreach (var genreId in dto.Genres)
                result.Add(new MovieGenre { GenreId = genreId });

            return result;
        }

        private IList<GenreDto> GenresMapping(Movie movie, GetMovieDetailsDto dto)
        {
            var result = new List<GenreDto>();

            if (movie.MovieGenres is null)
                return result;

            foreach(var genre in movie.MovieGenres)
            {
                result.Add(new GenreDto
                {
                    Id = genre.GenreId,
                    Name = genre.Genre.Name
                });
            }

            return result;
        }
        #endregion

        #region MovieCharacters
        private IList<MovieCharacter> MovieCharactersMapping(CreateMovieDto dto, Movie movie)
        {
            var result = new List<MovieCharacter>();

            if (dto.Characters is null)
                return result;

            foreach (var characterId in dto.Characters)
                result.Add(new MovieCharacter { CharacterId = characterId });

            return result;
        }

        private IList<MovieDto> MoviesMapping(Character character, GetCharacterDetailsDto dto)
        {
            var result = new List<MovieDto>();

            if (character.MovieCharacters is null)
                return result;

            foreach(var movie in character.MovieCharacters)
            {
                result.Add(new MovieDto
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title
                });
            }

            return result;
        }

        private IList<CharacterDto> CharactersMapping(Movie movie, GetMovieDetailsDto dto)
        {
            var result = new List<CharacterDto>();

            if (movie.MovieCharacters is null)
                return result;

            foreach(var character in movie.MovieCharacters)
            {
                result.Add(new CharacterDto
                {
                    Id = character.CharacterId,
                    Name = character.Character.Name
                });
            }

            return result;
        }
        #endregion
    }
}
