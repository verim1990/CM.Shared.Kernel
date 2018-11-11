using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CM.Shared.Kernel.Application.Validators
{
    public class MinCollectionLengthAttribute : ValidationAttribute
    {
        private readonly int _length;

        public MinCollectionLengthAttribute(int length)
        {
            _length = length;
        }

        public override bool IsValid(object value)
        {
            if (value is IList list)
                return list.Count >= _length;

            return false;
        }
    }
}