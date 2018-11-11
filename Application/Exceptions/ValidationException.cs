using System;
using System.Collections.Generic;

namespace CM.Shared.Kernel.Application.Exceptions
{
    [Serializable]
    public class ValidationException : AppException
    {
        public ValidationException(string key, string value) : base(value)
        {
           Errors = new List<KeyValuePair<string, string[]>>
           {
               new KeyValuePair<string, string[]>(key, new [] { value })
           }; 
        }

        public ValidationException(IList<KeyValuePair<string, string[]>> errors) : base("")
        {
            Errors = errors;
        }

        public IList<KeyValuePair<string, string[]>> Errors { get; protected set; }
    }
}