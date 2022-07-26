using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using System.Security.Claims;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-currencies")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MembershipCurrencyController : Controller
    {
        private MemberCurrencyService memberCurrencyService;
        private readonly IAuthorizationService _authorizationService;
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        public MembershipCurrencyController(MemberCurrencyService memberCurrencyService, IAuthorizationService authorizationService)
        {
            this.memberCurrencyService = memberCurrencyService;
            this._authorizationService = authorizationService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };
                return Ok(memberCurrencyService.GetMembershipCurrencies(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{membershipId}/{currencyId}")]
        public async Task<IActionResult> GetMembershipCurrency(int membershipId, int currencyId)
        {
            try
            {
                var memberCurrency = memberCurrencyService.GetMembershipCurrency(membershipId, currencyId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, memberCurrency, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(memberCurrency);
                }
                return Unauthorized();
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
        [Authorize(Role.Admin)]
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
        [HttpPut("{membershipId}/{currencyId}")]
        [Authorize(Role.Admin)]
        public IActionResult UpdateMembershipCurrency([FromBody] MembershipCurrency membershipCurrency, int membershipId, int currencyId)
        {
            try
            {
                bool result = memberCurrencyService.UpdateMembershipCurrency(membershipCurrency, membershipId, currencyId);
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
