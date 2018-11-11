using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace CM.Shared.Kernel.Application.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        private readonly Dictionary<string, string> _params = new Dictionary<string, string>();

        public AppException()
        {
        }

        public AppException(string message)
            : base(message)
        {
        }

        public AppException(string message, string translationKey) : base(message)
        {
            TranslationKey = translationKey.Length == 0 ? message : translationKey;
        }

        public AppException(string message, string translationKey, Exception inner)
            : base(message, inner)
        {
            TranslationKey = translationKey;
        }

        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string TranslationKey { get; protected set; }


        public IReadOnlyDictionary<string, string> MessageParams => new ReadOnlyDictionary<string, string>(_params);

        protected void AddMessageParam(string key, string value)
        {
            _params.Add(string.Format("##{0}##", key), value);
        }
    }
}