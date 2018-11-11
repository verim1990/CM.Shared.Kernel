using System.Collections.Generic;
using System.Net;

namespace CM.Shared.Kernel.Application.Responses
{
    public class ValidationResponse : CommonResponse
    {
        public ValidationResponse(IList<KeyValuePair<string, string[]>> errors) : base(HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }

        public IList<KeyValuePair<string, string[]>> Errors { get; protected set; }
    }
}