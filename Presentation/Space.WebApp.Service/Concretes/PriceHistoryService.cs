using RestSharp;
using System.Net;
using Space.RestHelper;
using Space.WebApp.Models;
using Space.RestHelper.Models;
using Space.Application.Helper;
using Space.RestHelper.Concretes;
using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.WebApp.Service.Interfaces;
using Space.Application.Models.Response;

namespace Space.WebApp.Service.Concretes
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly PriceApiUrl _priceApiUrl;
        private readonly SendRestRequest _sendRestRequest;

        public PriceHistoryService()
        {
            _priceApiUrl ??= ReadConfig.Get<PriceApiUrl>("PriceApi");
            _sendRestRequest ??= new SendRestRequest(_priceApiUrl.BaseUrl);
        }

        public async Task<BaseResponse<List<PriceHistoryResponse>>> GetPriceHistory(PriceHistoryRequest request)
        {
            var response = new BaseResponse<List<PriceHistoryResponse>>();

            var serviceResponse = await _sendRestRequest.RunAsync<BaseResponse<List<PriceHistoryResponse>>>(new RestRequestModel()
            {
                Endpoint = _priceApiUrl.Endpoint.PriceHistory,
                Data = request,
                Method = Method.Get,
                Content = new JsonContent(),
                Authenticator = new BearerAuthenticator().Authenticate(request.Token)
            });

            if (serviceResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.SetMessage("Unauthorized");
                return response;
            }

            if (serviceResponse.StatusCode != HttpStatusCode.OK)
            {
                response.SetMessage("Request is failed");
                return response;
            }

            if (!serviceResponse.Data.IsSuccess)
            {
                response.SetMessage(serviceResponse.Data.Message);
                return response;
            }

            response = serviceResponse.Data;

            return response;
        }
    }
}