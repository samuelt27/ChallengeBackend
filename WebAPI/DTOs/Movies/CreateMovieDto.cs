using ChallengeBackend.WebAPI.Helpers;
using ChallengeBackend.WebAPI.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend.WebAPI.DTOs.Movies
{
    public record CreateMovieDto
    {
        [Required]
        [MaxLength(120)]
        public string Title { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Release { get; init; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; init; }

        [Required]
        [FileTypeValidation(fileTypes: FileTypes.Image)]
        [FileSizeValidation(maxSizeInMegabytes: 6)]
        public IFormFile Image { get; init; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> Genres { get; init; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> Characters { get; init; }
    }
}
