using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Shared.Kernel.Application.Validators
{
    
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        public DateGreaterThanAttribute(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? earlierDate = (DateTime?)value;
            DateTime? laterDate = (DateTime?)validationContext.ObjectType
                .GetProperty(DateToCompareToFieldName)
                .GetValue(validationContext.ObjectInstance, null);

            if (!earlierDate.HasValue || !laterDate.HasValue || laterDate > earlierDate)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage ?? "Date is not later");
        }
    }
}
