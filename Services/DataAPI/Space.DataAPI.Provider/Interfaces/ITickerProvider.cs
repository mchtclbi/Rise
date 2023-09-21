using Space.DataAPI.Provider.Concretes;
using Space.DataAPI.Provider.Models.Ticker.Request;
using Space.DataAPI.Provider.Models.Ticker.Response;

namespace Space.DataAPI.Provider.Interfaces
{
    public interface ITickerProvider
    {
        public Task<ProviderResponse<GetPriceWithProviderResponse>> GetPrice(GetPriceWithProviderRequest request);
    }
}