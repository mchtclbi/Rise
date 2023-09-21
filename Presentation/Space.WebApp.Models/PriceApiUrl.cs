namespace Space.WebApp.Models
{
    public class PriceApiUrl
    {
        public string BaseUrl { get; set; }
        public PriceApiEndpoint Endpoint { get; set; }
    }

    public class PriceApiEndpoint
    {
        public string PriceHistory { get; set; }
    }
}