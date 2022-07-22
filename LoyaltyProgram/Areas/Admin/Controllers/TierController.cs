using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;
using Newtonsoft.Json;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/tiers")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TierController : Controller
    {
        private TierService tierService;
        public TierController(TierService tierService)
        {
            this.tierService = tierService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };     

                var tiers = tierService.GetTiers(pagingParameters);
                var metadata = new
                {
                    tiers.TotalCount,
                    tiers.PageSize,
                    tiers.CurrentPage,
                    tiers.TotalPages,
                    tiers.HasNext,
                    tiers.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(tiers);
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("program")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy, int programId)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };

                var tiers = tierService.GetTiers(pagingParameters, programId);
                var metadata = new
                {
                    tiers.TotalCount,
                    tiers.PageSize,
                    tiers.CurrentPage,
                    tiers.TotalPages,
                    tiers.HasNext,
                    tiers.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(tiers);
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("program/{id}")]
        public IActionResult GetTierByProgramId(int id)
        {
            try
            {
                return Ok(tierService.GetTiers(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("find-all")]
        public IActionResult GetTiers()
        {
            try
            {
                return Ok(tierService.GetTiers());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetTierByID(int id)
        {
            try
            {
                return Ok(tierService.GetTierByID(id));
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
                return Ok(tierService.GetTierCount());
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
        public IActionResult DeleteTier(int id)
        {
            try
            {
                bool result = tierService.DeleteTier(id);
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
        public IActionResult UpdateTier([FromBody] Tier tier, int id)
        {
            try
            {
                bool result = tierService.UpdateTier(tier, id);
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
        public IActionResult AddTier([FromBody] Tier tier)
        {
            try
            {
                bool result = tierService.AddTier(tier);
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
