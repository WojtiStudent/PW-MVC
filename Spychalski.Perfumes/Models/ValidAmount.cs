using System.ComponentModel.DataAnnotations;

namespace Spychalski.Perfumes.Models
{
    public class ValidAmount: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Perfume)validationContext.ObjectInstance;
            int _amount = (int)value;
            StatusType _status = model.Status;

            if (StatusType.InStock == _status && _amount < 1)
            {
                return new ValidationResult(
                    "Amount must be greater than 0 when status is In Stock.");
            }
            else if (StatusType.OutOfStock == _status && _amount > 0)
            {
                return new ValidationResult(
                    "Amount must be 0 when status is Out of Stock.");
            }
            else if (StatusType.Ordered == _status && _amount > 0)
            {
                return new ValidationResult(
                    "Amount must be 0 when status is Ordered.");
            }
            
            return ValidationResult.Success;
            
        }
    }
}
