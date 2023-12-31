using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LaterThanAttribute : ValidationAttribute
    {
        private readonly string _otherPropertyName;

        public LaterThanAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_otherPropertyName}.");
            }

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherValue != null && (DateTime)value <= (DateTime)otherValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be later than {_otherPropertyName}.");
            }

            return ValidationResult.Success;
        }
    }
}
