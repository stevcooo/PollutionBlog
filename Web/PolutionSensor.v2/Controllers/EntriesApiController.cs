using Microsoft.AspNetCore.Mvc;
using PolutionSensor.v2.Models;
using PolutionSensor.v2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PolutionSensor.v2.Controllers
{
    [Route("api/Entries")]
    [ApiController]
    public class EntriesApiController : ControllerBase
    {
        private IEntriesService _service;

        public EntriesApiController(IEntriesService service)
        {
            _service = service;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<IEnumerable<Entry>> GetEntries()
        {
            return await _service.GetAllAsync();
        }

        // POST: api/Entries
        [HttpPost]
        public async Task<IActionResult> PostEntry([FromBody] Entry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddAsync(entry);
            return CreatedAtAction("GetEntry", new { id = entry.EntryId }, entry);
        }
    }
}