using RestSharp;
using System.Net;
using Space.RestHelper;
using Space.RestHelper.Models;
using Space.Application.Helper;
using Space.RestHelper.Concretes;
using Space.DataAPI.Provider.Models;
using Space.DataAPI.Provider.Concretes;
using Space.DataAPI.Provider.Interfaces;
using Space.DataAPI.Provider.BTCTurk.Models;
using Space.DataAPI.Provider.Models.Ticker.Request;
using Space.DataAPI.Provider.Models.Ticker.Response;

namespace Space.DataAPI.Provider.BTCTurk
{
    public class BTCTurkIntegration : ITickerProvider
    {
        private readonly ConfigModel _configModel;
        private readonly SendRestRequest _sendRestRequest;

        public BTCTurkIntegration()
        {
            _configModel ??= ReadConfig.Get<ConfigModel>(new List<string>() { "Providers", "BTCTurk" });
            _sendRestRequest ??= new SendRestRequest(_configModel.BaseURL);
        }

        public async Task<ProviderResponse<GetPriceWithProviderResponse>> GetPrice(GetPriceWithProviderRequest request)
        {
            var response = new ProviderResponse<GetPriceWithProviderResponse>();

            var providerResponse = await _sendRestRequest.RunAsync<GetTickerResponse>(new RestRequestModel()
            {
                Endpoint = _configModel.Endpoint.Ticker,
                Method = Method.Get,
                Content = new QueryContent()
            });

            if (providerResponse.StatusCode != HttpStatusCode.OK)
                return response.SetErrorAndReturn("", ErrorType.BadRequest, providerResponse.StatusCode);

            response.IsSuccess = true;
            response.Data = new GetPriceWithProviderResponse()
            {
                Price = providerResponse.Data.Data.First().Last
            };

            return response;
        }
    }
}