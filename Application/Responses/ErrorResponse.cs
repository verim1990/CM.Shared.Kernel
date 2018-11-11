using System.Net;

namespace CM.Shared.Kernel.Application.Responses
{
    public class ErrorResponse : CommonResponse
    {
        public ErrorResponse(HttpStatusCode statusCode, string message) : base(statusCode)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}