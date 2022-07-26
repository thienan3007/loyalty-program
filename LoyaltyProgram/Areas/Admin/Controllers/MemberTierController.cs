using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using System.Security.Claims;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/member-tiers")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class MemberTierController : Controller
    {
        private readonly MemberTierService memberTierService;
        private readonly IAuthorizationService _authorizationService;
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        public MemberTierController(MemberTierService memberTierService, IAuthorizationService authorizationService)
        {
            this.memberTierService = memberTierService;
            this._authorizationService = authorizationService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult GetMemberTiers(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };
                return Ok(memberTierService.GetMemberTiers(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{loyaltyTierId}/{loyaltyMembershipId}")]
        public async Task<IActionResult> GetMemberTier(int loyaltyTierId, int loyaltyMembershipId)
        {
            try
            {
                var memberTier = memberTierService.GetMemberTier(loyaltyMembershipId, loyaltyTierId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, memberTier, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(memberTier);
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
        [HttpGet("current/{loyaltyMembershipId}")]
        public async Task<IActionResult> GetCurrentMemberTier(int loyaltyMembershipId)
        {
            try
            {
                var memberTier = memberTierService.GetMemberTierCurrent(loyaltyMembershipId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, memberTier, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(memberTier);
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
        [HttpGet("{loyaltyMembershipId}")]
        public async Task<IActionResult> GetMemberTiers(int loyaltyMembershipId, int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {

                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString,  OrderBy = orderBy};
                var memberTiers = memberTierService.GetMemberTiers(loyaltyMembershipId, pagingParameters);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, memberTiers, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(memberTiers);
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
        [HttpPost("")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPut("{loyaltyMembershipId}/{loyaltyTierId}")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{loyaltyMembershipId}/{loyaltyTierId}")]
        [Authorize(Role.Admin)]
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
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count")]
        [Authorize(Role.Admin)]
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
