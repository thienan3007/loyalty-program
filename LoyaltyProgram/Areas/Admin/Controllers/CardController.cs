using LoyaltyProgram.Auth;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using LoyaltyProgram.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/cards")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardController : Controller
    {
        private CardService cardService;
        private readonly IAuthorizationService _authorizationService;
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        public CardController(CardService cardService, IAuthorizationService authorizationService)
        {
            this.cardService = cardService;
            _authorizationService = authorizationService;
        }
        [Authorize(Role.Admin)]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy};
                var cards = cardService.GetCards(pagingParameters);

                if (cards != null)
                {
                    return Ok(cards);
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
        [HttpGet("{memberId}")]
        public async Task<IActionResult> Get(int memberId)
        {
            try
            {
                var card = cardService.GetCardById(memberId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, card, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(card);
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize(Role.Admin)]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            try
            {
                return Ok(cardService.GetCount());
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
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = cardService.Delete(id);
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
        public IActionResult Update([FromBody] Card card, int id)
        {
            try
            {
                bool result = cardService.Update(card, id);
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
        public IActionResult Add([FromBody] Card card)
        {
            try
            {
                bool result = cardService.Add(card);
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
