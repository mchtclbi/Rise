using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.Application.Models.Response;

namespace Space.WebApp.Service.Interfaces
{
    public interface IPriceHistoryService
    {
        public Task<BaseResponse<List<PriceHistoryResponse>>> GetPriceHistory(PriceHistoryRequest request);
    }
}