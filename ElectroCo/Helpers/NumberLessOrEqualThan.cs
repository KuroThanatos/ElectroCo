using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCo.Helpers
{
    public class NumberLessOrEqualThan : ValidationAttribute
    {
        private string _startNumberPropertyName;
        public NumberLessOrEqualThan(string startNumberPropertyName)
        {
            _startNumberPropertyName = startNumberPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_startNumberPropertyName);
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (propertyInfo == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", _startNumberPropertyName));
            }

            if ((int)value <= (int)propertyValue)
            {
                return ValidationResult.Success;
            }
            else
            {

                var startNumberDisplayName = propertyInfo
                    .GetCustomAttributes(typeof(DisplayAttribute), true)
                    .Cast<DisplayAttribute>()
                    .Single()
                    .Name;

                return new ValidationResult(validationContext.DisplayName + " deve ser menor que " + startNumberDisplayName + ".");
            }
        }
    }
}
