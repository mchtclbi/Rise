namespace Space.DataAPI.Provider.BTCTurk.Models
{
    public class ConfigModel
    {
        public string BaseURL { get; set; }
        public Endpoint Endpoint { get; set; }
    }

    public class Endpoint
    {
        public string Ticker { get; set; }
    }
}