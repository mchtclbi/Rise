using System.Net;

namespace Space.DataAPI.Provider.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public ErrorType ErrorType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}