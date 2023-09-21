using Space.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Space.WebApp.Models.Request;
using Space.WebApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Space.WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPriceHistoryService _priceHistoryService;

        public HomeController(ILogger<HomeController> logger, IPriceHistoryService priceHistoryService)
        {
            _logger = logger;
            _priceHistoryService = priceHistoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceHistory([FromQuery] DateRange dateRange)
        {
            var request = GetPriceHistoryRequestWithDateRange(dateRange);
            if (request == null)
                return Json(new { isSuccess = false, message = "date range is not read" });

            request.Token = Request.HttpContext.Session.GetString(Constant.CookieKey);

            return Json(await _priceHistoryService.GetPriceHistory(request));
        }

        private PriceHistoryRequest? GetPriceHistoryRequestWithDateRange(DateRange dateRange)
        {
            var currentDate = DateTime.Now;

            if (dateRange == DateRange.LastDay)
                return new PriceHistoryRequest() { StartDate = currentDate.AddDays(-1), EndDate = currentDate };

            if (dateRange == DateRange.LastWeek)
                return new PriceHistoryRequest() { StartDate = currentDate.AddDays(-7), EndDate = currentDate };

            if (dateRange == DateRange.LastMonth)
                return new PriceHistoryRequest() { StartDate = currentDate.AddMonths(-1), EndDate = currentDate };

            if (dateRange == DateRange.LastSixMonths)
                return new PriceHistoryRequest() { StartDate = currentDate.AddMonths(-6), EndDate = currentDate };

            return null;
        }
    }
}