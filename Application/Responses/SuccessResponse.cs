using System.Net;

namespace CM.Shared.Kernel.Application.Responses
{
    public class SuccessResponse : CommonResponse
    {
        public SuccessResponse(object result) : base(HttpStatusCode.OK)
        {
            Result = result;
        }

        public object Result { get; set; }
    }
}