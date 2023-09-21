using Space.DataAPI.Models.Request;
using Space.DataAPI.Models.Response;
using Space.Application.Models.Response;

namespace Space.DataAPI.Services.Interfaces
{
    public interface IPriceHistoryService
    {
        public Task<BaseResponse<AddPriceHistoryResponse>> Add(AddPriceHistoryRequest request);
        public Task<BaseResponse<List<GetPriceHistoryResponse>>> Get(GetPriceHistoryRequest request);
    }
}