using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ChallengeBackend.WebAPI.Helpers
{
    public class FileSizeValidation : ValidationAttribute
    {
        private readonly int _maxSizeInMegabytes;


        public FileSizeValidation(int maxSizeInMegabytes)
        {
            _maxSizeInMegabytes = maxSizeInMegabytes;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if(formFile is null)
                return ValidationResult.Success;

            if (formFile.Length > _maxSizeInMegabytes * 1024 * 1024)
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_maxSizeInMegabytes} MB.");

            return ValidationResult.Success;
        }
    }
}
