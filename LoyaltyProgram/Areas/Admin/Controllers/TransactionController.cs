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
    [Route("api/v{version:apiVersion}/transactions")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TransactionController : Controller
    {
        private TransactionService transactionService;
        private readonly IAuthorizationService _authorizationService;
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        public TransactionController(TransactionService transactionService, IAuthorizationService authorizationService)
        {
            this.transactionService = transactionService;
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
                return Ok(transactionService.GetTransactions(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var transaction = transactionService.GetTransaction(id);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, transaction, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(transaction);
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
        [HttpGet("member/{membershipID}")]
        public async Task<IActionResult> GetByMemberId(int membershipID, int pageNumber, int pageSize, string? orderBy)
        {
            try
            {

                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy };
                var transactions = transactionService.GetTransactionsByMember(membershipID, pagingParameters);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, transactions, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(transactions);
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
                return Ok(transactionService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count/{memberId}")]
        public async Task<IActionResult> GetCountByMember(int memberId)
        {
            try
            {
                var count = transactionService.GetCount(memberId);
                var transactions = transactionService.GetAllTransactionsByMember(memberId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, transactions, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(new { Count = count});
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
        [HttpDelete("{id}")]
        [Authorize(Role.Admin)]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = transactionService.Delete(id);
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
        public IActionResult Update([FromBody] Transaction transaction, int id)
        {
            try
            {
                bool result = transactionService.Update(transaction, id);
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
        public IActionResult Add([FromBody] Transaction transaction)
        {
            try
            {
                bool result = transactionService.Add(transaction);
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
