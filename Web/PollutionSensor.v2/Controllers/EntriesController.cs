using Microsoft.AspNetCore.Mvc;
using PollutionSensor.v2.Services;
using System.Threading.Tasks;

namespace PollutionSensor.v2.Controllers
{
    public class EntriesController : Controller
    {
        private IEntriesService _service;

        public EntriesController(IEntriesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetLast100Async());
        }
    }
}