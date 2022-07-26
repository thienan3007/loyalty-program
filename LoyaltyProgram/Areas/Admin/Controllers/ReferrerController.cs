using System.Security.Claims;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;

namespace LoyaltyProgram.Areas.Admin.Controllers
{

    [Route("api/v{version:apiVersion}/referrers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReferrerController : ControllerBase
    {
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        private readonly IAuthorizationService _authorizationService;
        private readonly ReferrerService _referrerService;
        private readonly MembershipService _membershipService;

        public ReferrerController(IAuthorizationService authorizationService, ReferrerService referrerService, MembershipService membershipService)

        {
            _authorizationService = authorizationService;
            _referrerService = referrerService;
            _membershipService = membershipService;

        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPost("send")]
        [Authorize(Role.User)]
        public async Task<IActionResult> SendReferrer(string memberCode)
        {
            try
            {
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                var member = _membershipService.GetMembershipById(applicationUser.Id);
                if (member != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity();
                    identity.AddClaim(new Claim("email", applicationUser.Email));
                    identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                    principal.AddIdentity(identity);
                    var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, member, Operations.Read);
                    if (authorizatonResult.Succeeded)
                    {
                        var ok = _referrerService.CheckReferrer(applicationUser.Id);
                        if (!ok)
                        {
                            var result = _referrerService.AddReferrer(memberCode, applicationUser.Id);

                            if (result != null)
                            {
                                return Ok(result);
                            }

                            return BadRequest("This membership code is not existed!");
                        }

                        return BadRequest("This account already had referrer");
                    }
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
