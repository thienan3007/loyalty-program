using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/event-sources")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EventSourceController : Controller
    {
        private EventSourceService eventSourceService;
        public EventSourceController(EventSourceService eventSourceService)
        {
            this.eventSourceService = eventSourceService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(eventSourceService.GetEventSources());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetEventSource(int id)
        {
            try
            {
                return Ok(eventSourceService.GetEventSource(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            try
            {
                return Ok(eventSourceService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteEventSource(int id)
        {
            try
            {
                bool result = eventSourceService.DeleteEventSource(id);
                if (result)
                    return Ok("Successful");
                return BadRequest("Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] EventSource eventSource, int id)
        {
            try
            {
                bool result = eventSourceService.UpdateEventSource(eventSource, id);
                if (result)
                    return Ok("Successful");
                return BadRequest("Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult Add([FromBody] EventSource eventSource)
        {
            try
            {
                bool result = eventSourceService.AddEventSource(eventSource);
                if (result)
                    return Ok("Successful");
                return BadRequest("Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
