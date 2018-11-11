using System;
using System.Net;

namespace CM.Shared.Kernel.Application.Responses
{
    public class CommonResponse
    {
        public CommonResponse(HttpStatusCode statusCode)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
        }

        public string Version => "1.2.3";

        public int StatusCode { get; set; }

        public string RequestId { get; }
    }
}