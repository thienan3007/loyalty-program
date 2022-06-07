using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-tiers")]
    [ApiVersion("1.0")]
    public class MemberTierController : Controller
    {
        private readonly MemberTierService memberTierService;
        public MemberTierController(MemberTierService memberTierService)
        {
            this.memberTierService = memberTierService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult GetMemberTiers()
        {
            try
            {
                return Ok(memberTierService.GetMemberTiers());
            } catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{loyaltyTierId}/{loyaltyMembershipId}")]
        public IActionResult GetMemberTier(int loyaltyTierId, int loyaltyMembershipId)
        {
            try
            {
                return Ok(memberTierService.GetMemberTier(loyaltyMembershipId, loyaltyTierId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{loyaltyMembershipId}")]
        public IActionResult GetMemberTiers(int loyaltyMembershipId)
        {
            try
            {
                return Ok(memberTierService.GetMemberTiers(loyaltyMembershipId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult AddMemberTier([FromBody] MemberTier memberTier)
        {
            try
            {
                bool result = memberTierService.AddMemberTier(memberTier);
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
        [HttpPut("{loyaltyMembershipId}/{loyaltyTierId}")]
        public IActionResult UpdateMemberTier([FromBody] MemberTier memberTier, int loyaltyMembershipId, int loyaltyTierId)
        {
            try
            {
                var result = memberTierService.UpdateMemberTier(memberTier, loyaltyMembershipId, loyaltyTierId);
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
        [HttpDelete("{loyaltyMembershipId}/{loyaltyTierId}")]
        public IActionResult DeleteMemberTier(int loyaltyMembershipId, int loyaltyTierId)
        {
            try
            {
                var result = memberTierService.DeleteMemberTier(loyaltyMembershipId, loyaltyTierId);
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
        [HttpGet("count")]
        public IActionResult GetMemberTierCount()
        {
            try
            {
                return Ok(memberTierService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
