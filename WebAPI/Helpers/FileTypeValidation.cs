using ChallengeBackend.WebAPI.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChallengeBackend.WebAPI.Helpers
{
    public class FileTypeValidation : ValidationAttribute
    {
        private readonly string[] _validFormats;


        public FileTypeValidation(string[] validFormats)
        {
            _validFormats = validFormats;
        }

        public FileTypeValidation(FileTypes fileTypes)
        {
            if(fileTypes == FileTypes.Image)
                _validFormats = new string[] { "image/jpeg", "image/png" };
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile is null)
                return ValidationResult.Success;

            if (!_validFormats.Contains(formFile.ContentType))
                return new ValidationResult($"El formato del archivo debe ser: {string.Join(", ", _validFormats)}");

            return ValidationResult.Success;
        }
    }
}
