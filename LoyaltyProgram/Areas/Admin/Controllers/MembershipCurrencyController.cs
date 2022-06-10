using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-currencies")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MembershipCurrencyController : Controller
    {
        private MemberCurrencyService memberCurrencyService;
        public MembershipCurrencyController(MemberCurrencyService memberCurrencyService)
        {
            this.memberCurrencyService = memberCurrencyService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(memberCurrencyService.GetMembershipCurrencies());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetMembershipCurrency(int id)
        {
            try
            {
                return Ok(memberCurrencyService.GetMembershipCurrency(id));
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
                return Ok(memberCurrencyService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMembershipCurrency(int id)
        {
            try
            {
                bool result = memberCurrencyService.DeleteMembershipCurrency(id);
                if (result)             
                    return Ok("Successful");
                return BadRequest("Cannot find this record");
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPut("{id}")]
        public IActionResult UpdateMembershipCurrency([FromBody] MembershipCurrency membershipCurrency, int id)
        {
            try
            {
                bool result = memberCurrencyService.UpdateMembershipCurrency(membershipCurrency, id);
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
        public IActionResult AddMembershipCurrency([FromBody] MembershipCurrency membershipCurrency)
        {
            try
            {
                bool result = memberCurrencyService.AddMembershipCurrency(membershipCurrency);
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
