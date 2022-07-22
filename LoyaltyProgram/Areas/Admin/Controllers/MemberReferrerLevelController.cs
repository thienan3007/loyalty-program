using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-referrer-levels")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class MemberReferrerLevelController : Controller
    {
        private MemberReferrerLevelService memberReferrerLevelService;
        public MemberReferrerLevelController(MemberReferrerLevelService memberReferrerLevelService)
        {
            this.memberReferrerLevelService = memberReferrerLevelService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll(int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy };
                return Ok(memberReferrerLevelService.GetMemberReferrerLevels(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPost("")]
        [Authorize(Role.Admin)]
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
