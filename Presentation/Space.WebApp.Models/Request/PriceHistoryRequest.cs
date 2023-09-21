using Space.Application.Models.Request;

namespace Space.WebApp.Models.Request
{
    public class PriceHistoryRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}