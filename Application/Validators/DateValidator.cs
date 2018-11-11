using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CM.Shared.Kernel.Application.Validators
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime date;
        }
    }
}