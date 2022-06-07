using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/tiers")]
    [ApiVersion("1.0")]
    public class TierController : Controller
    {
        private TierService tierService;
        public TierController(TierService tierService)
        {
            this.tierService = tierService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(tierService.GetTiers());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetTierByID(int id)
        {
            try
            {
                return Ok(tierService.GetTierByID(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            try
            {
                return Ok(tierService.GetTierCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteTier(int id)
        {
            try
            {
                bool result = tierService.DeleteTier(id);
                if (result)
                    return Ok("Successful");
                return BadRequest("Failed");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPut("{id}")]
        public IActionResult UpdateTier([FromBody] Tier tier, int id)
        {
            try
            {
                bool result = tierService.UpdateTier(tier, id);
                if (result)
                    return Ok("Successful");
                return BadRequest("Failed");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult AddTier([FromBody] Tier tier)
        {
            try
            {
                bool result = tierService.AddTier(tier);
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
