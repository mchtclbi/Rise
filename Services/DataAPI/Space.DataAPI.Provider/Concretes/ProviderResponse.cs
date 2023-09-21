using System.Net;
using Space.DataAPI.Provider.Models;
using Space.DataAPI.Provider.Interfaces;

namespace Space.DataAPI.Provider.Concretes
{
    public class ProviderResponse<T> : IProviderResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorModel ErrorModel { get; set; }
        public List<LogModel> Logs { get; set; } = new List<LogModel>();

        public void SetError(string message, ErrorType errorType, HttpStatusCode statusCode, Exception exception = null)
        {
            IsSuccess = false;
            ErrorModel = new ErrorModel()
            {
                Message = message,
                ErrorType = errorType,
                StatusCode = statusCode,
                Exception = exception
            };
        }

        public ProviderResponse<T> SetErrorAndReturn(string message, ErrorType errorType, HttpStatusCode statusCode, Exception exception = null)
        {
            IsSuccess = false;
            ErrorModel = new ErrorModel()
            {
                Message = message,
                ErrorType = errorType,
                StatusCode = statusCode,
                Exception = exception
            };

            return this;
        }

        public void AddLog(string request, string response)
        {
            Logs.Add(new LogModel()
            {
                Request = request,
                Response = response
            });
        }
    }
}