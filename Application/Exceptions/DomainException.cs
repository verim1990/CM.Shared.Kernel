using System;
using System.Runtime.Serialization;

namespace CM.Shared.Kernel.Application.Exceptions
{
    [Serializable]
    public class DomainException : AppException
    {
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, string translationKey)
            : base(message, translationKey)
        {
        }


        public DomainException(string message, string translationKey, Exception inner)
            : base(message, translationKey, inner)
        {
        }

        protected DomainException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}