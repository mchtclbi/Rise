using Microsoft.AspNetCore.Mvc;
using Space.DataAPI.Models.Request;
using Space.DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Space.Data.API.Controllers
{
    //[Authorize]
    public class PriceHistoryController : Controller
    {
        private readonly IPriceHistoryService _service;

        public PriceHistoryController(IPriceHistoryService priceHistoryService)
        {
            _service = priceHistoryService;
        }

        [HttpGet("api/price/histories")]
        public async Task<IActionResult> GetPriceHistories([FromBody] GetPriceHistoryRequest request) => Ok(await _service.Get(request));
    }
}