using Space.DataAPI.Data.Entities;
using Space.DataAPI.Models.Request;
using Space.DataAPI.Data.Interfaces;
using Space.DataAPI.Models.Response;
using Space.Application.Models.Response;
using Space.DataAPI.Services.Interfaces;

namespace Space.DataAPI.Services.Concretes
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IPriceHistoryRepository _repository;

        public PriceHistoryService(IPriceHistoryRepository priceHistory)
        {
            _repository = priceHistory;
        }

        public async Task<BaseResponse<AddPriceHistoryResponse>> Add(AddPriceHistoryRequest request)
        {
            var response = new BaseResponse<AddPriceHistoryResponse>();

            try
            {
                await _repository.Add(new PriceHistory()
                {
                    Price = request.Price,
                    CreatedDate = DateTime.Now,
                });

                response.IsSuccess = true;
            }
            catch (Exception)
            {
                response.SetMessage("Please try again later!");
            }

            return response;
        }

        public async Task<BaseResponse<List<GetPriceHistoryResponse>>> Get(GetPriceHistoryRequest request)
        {
            var response = new BaseResponse<List<GetPriceHistoryResponse>>();

            try
            {
                var data = await _repository.GetAll(q => q.CreatedDate >= request.StartDate && q.CreatedDate <= request.EndDate);

                response.IsSuccess = true;
                response.Data = data.Select(q => new GetPriceHistoryResponse()
                {
                    Price = q.Price,
                    Date = q.CreatedDate
                }).ToList();
            }
            catch (Exception)
            {
                response.SetMessage("Please try again later!");
            }

            return response;
        }
    }
}