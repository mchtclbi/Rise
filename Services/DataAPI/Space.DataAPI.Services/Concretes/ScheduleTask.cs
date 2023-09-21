using Space.DataAPI.Models.Request;
using Space.DataAPI.Services.Factory;
using Space.DataAPI.Provider.Interfaces;
using Space.DataAPI.Services.Interfaces;
using Space.DataAPI.Provider.Declarations;
using Space.DataAPI.Provider.Models.Ticker.Request;

namespace Space.DataAPI.Services.Concretes
{
    public class ScheduleTask : IScheduleTask
    {
        private readonly ITickerProvider _tickerProvider;
        private readonly IPriceHistoryService _priceHistoryService;

        public ScheduleTask(IPriceHistoryService priceHistoryService)
        {
            _priceHistoryService = priceHistoryService;
            _tickerProvider ??= new GetTickerProviderFactory().GetProvider(TickerProvider.BTCTurk);
        }

        public async Task AddPriceHistory()
        {
            var tickerResponse = await _tickerProvider.GetPrice(new GetPriceWithProviderRequest());
            if (!tickerResponse.IsSuccess) return;

            await _priceHistoryService.Add(new AddPriceHistoryRequest()
            {
                Price = tickerResponse.Data.Price
            });
        }
    }
}