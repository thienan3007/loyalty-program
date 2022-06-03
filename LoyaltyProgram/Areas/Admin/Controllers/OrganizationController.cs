using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/organizations")]
    [ApiVersion("1.0")]
    public class OrganizationController : Controller
    {
        private OrganizationService organizationService;
        public OrganizationController(OrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [Produces("application/json")]
        [HttpGet("get-organizations")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(organizationService.GetOrganizations());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("get-organization/{id}")]
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

        [Produces("application/json")]
        [HttpGet("get-count")]
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

        [Produces("application/json")]
        [HttpGet("delete-organization/{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                bool result = organizationService.DeleteOrganization(id);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPut("update-organization/{id}")]
        public IActionResult UpdateBrand([FromBody] Organization organization, int id)
        {
            try
            {
                bool result = organizationService.UpdateOrganization(organization, id);
                return Ok(result ? "Success" : "Failed");   
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("add-organization")]
        public IActionResult AddBrand([FromBody] Organization organization)
        {
            try
            {
                bool result = organizationService.AddOrganization(organization);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
