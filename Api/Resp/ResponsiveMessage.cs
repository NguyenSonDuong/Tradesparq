using System.Numerics;

namespace Api.Resp
{
    public class ResponsiveMessage
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
