using Space.DataAPI.Provider.Models;

namespace Space.DataAPI.Provider.Interfaces
{
    public interface IProviderResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorModel ErrorModel { get; set; }
        public List<LogModel> Logs { get; set; }
    }
}