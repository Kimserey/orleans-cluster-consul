using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Guid gid && gid == Guid.Empty)
            {
                return new ValidationResult(GetErrorMessage(validationContext)); ;
            }
            return ValidationResult.Success;
        }
        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
            return $"{validationContext.DisplayName} can't be a empty guid.";
        }
    }
}
