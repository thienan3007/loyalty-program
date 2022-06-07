using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-referrer-levels")]
    [ApiVersion("1.0")]
    public class MemberReferrerLevelController : Controller
    {
        private MemberReferrerLevelService memberReferrerLevelService;
        public MemberReferrerLevelController(MemberReferrerLevelService memberReferrerLevelService)
        {
            this.memberReferrerLevelService = memberReferrerLevelService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(memberReferrerLevelService.GetMemberReferrerLevels());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetMemberReferrerLevel(int id)
        {
            try
            {
                return Ok(memberReferrerLevelService.GetMemberReferrerLevel(id));
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
                return Ok(memberReferrerLevelService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteLevel(int id)
        {
            try
            {
                bool result = memberReferrerLevelService.DeleteMemberReferrerLevel(id);
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
        public IActionResult UpdateLevel([FromBody] MemberReferrerLevel memberReferrerLevel, int id)
        {
            try
            {
                bool result = memberReferrerLevelService.UpdateMemberReferrerLevel(memberReferrerLevel, id);
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
        public IActionResult AddLevel([FromBody] MemberReferrerLevel memberReferrerLevel)
        {
            try
            {
                bool result = memberReferrerLevelService.AddMemberReferrerLevel(memberReferrerLevel);
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
