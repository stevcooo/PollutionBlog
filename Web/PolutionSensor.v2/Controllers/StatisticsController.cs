using Microsoft.AspNetCore.Mvc;
using PolutionSensor.v2.Services;
using System.Threading.Tasks;

namespace PolutionSensor.v2.Controllers
{
    public class StatisticsController : Controller
    {
        private IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetDailyStatisticsAsync());
        }        

        // GET: Statistics/Hourly/5
        public async Task<IActionResult> Hourly(string id)
        {
            if (id == null)
            {
                return NoContent();
            }
            return View(await _service.GetHourlyStatisticsAsync(id));
        }
    }
}