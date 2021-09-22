using ChallengeBackend.WebAPI.Helpers;
using ChallengeBackend.WebAPI.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend.WebAPI.DTOs.Characters
{
    public record CreateCharacterDto
    {
        private int _age;


        [Required]
        [MaxLength(120)]
        public string Name { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; init; }

        [Required]
        [MaxLength(1200)]
        public string Story { get; init; }

        [Required]
        [FileTypeValidation(fileTypes: FileTypes.Image)]
        [FileSizeValidation(maxSizeInMegabytes: 6)]
        public IFormFile Image { get; init; }

        public int Age
        {
            get
            {
                if (_age <= 0)
                    _age = new DateTime(DateTime.Now.Subtract(DateOfBirth).Ticks).Year - 1;

                return _age;
            }
        }
    }
}
