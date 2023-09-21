using Space.DataAPI.Provider.BTCTurk;
using Space.DataAPI.Provider.Interfaces;
using Space.DataAPI.Provider.Declarations;

namespace Space.DataAPI.Services.Factory
{
    public class GetTickerProviderFactory
    {
        public ITickerProvider GetProvider(TickerProvider tickerProvider) => tickerProvider switch
        {
            TickerProvider.BTCTurk => new BTCTurkIntegration(),
            _ => throw new NotImplementedException(),
        };
    }
}