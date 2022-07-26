using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/v{version:apiVersion}/organizations")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrganizationController : Controller
    {
        private OrganizationService organizationService;
        public OrganizationController(OrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };
                return Ok(organizationService.GetOrganizations(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetOrganization(int id)
        {
            try
            {
                return Ok(organizationService.GetOrganizationById(id));
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
                return Ok(organizationService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                bool result = organizationService.DeleteOrganization(id);
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
        public IActionResult UpdateBrand([FromBody] Organization organization, int id)
        {
            try
            {
                bool result = organizationService.UpdateOrganization(organization, id);
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
        public IActionResult AddBrand([FromBody] Organization organization)
        {
            try
            {
                bool result = organizationService.AddOrganization(organization);
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
