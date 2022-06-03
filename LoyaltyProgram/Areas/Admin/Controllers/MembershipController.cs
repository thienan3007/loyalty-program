using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/memberships")]
    [ApiVersion("1.0")]
    public class MembershipController : Controller
    {
        private MembershipService membershipService;
        public MembershipController(MembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(membershipService.GetMemberships());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetMembershipById(int id)
        {
            try
            {
                return Ok(membershipService.GetMembershipById(id));
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
                return Ok(membershipService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMembership(int id)
        {
            try
            {
                bool result = membershipService.DeleteMembership(id);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPut("{id}")]
        public IActionResult UpdateMembership([FromBody] Membership membership, int id)
        {
            try
            {
                bool result = membershipService.UpdateMembership(membership, id);
                return Ok(result ? "Success" : "Failed");   
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult AddMembership([FromBody] Membership membership)
        {
            try
            {
                bool result = membershipService.AddMembership(membership);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
