using System;
using System.Net;

namespace CM.Shared.Kernel.Application.Responses
{
    public class ErrorDevResponse : ErrorResponse
    {
        public ErrorDevResponse(HttpStatusCode statusCode, string message, string stackTrace) : base(statusCode, message)
        {
            StackTrace = stackTrace;
        }

        public string StackTrace{ get; set; }
    }
}