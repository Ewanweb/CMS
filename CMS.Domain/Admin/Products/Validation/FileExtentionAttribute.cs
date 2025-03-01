using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Admin.Products.Validation
{
    public class FileExtentionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extention = Path.GetExtension(file.FileName);

                string[] extentions = { "jpg", "png" };

                bool result = extentions.Any(x => extention.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("باید پسوند فایل .jpg یا .png باشه");
                }
            }

            return ValidationResult.Success;
        }
    }
}
