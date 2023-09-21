namespace Space.DataAPI.Models.Request
{
    public class GetPriceHistoryRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}