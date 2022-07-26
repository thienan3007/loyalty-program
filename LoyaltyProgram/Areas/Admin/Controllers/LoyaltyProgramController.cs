using LoyaltyProgram.Auth;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/programs")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class LoyaltyProgramController : Controller
    {
        private LoyaltyProgramService loyaltyProgramService;
        public LoyaltyProgramController(LoyaltyProgramService loyaltyProgramService)
        {
            this.loyaltyProgramService = loyaltyProgramService;
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
                return Ok(loyaltyProgramService.GetPrograms(pagingParameters));
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
        public IActionResult GetProgram(int id)
        {
            try
            {
                return Ok(loyaltyProgramService.GetProgramById(id));
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
                return Ok(loyaltyProgramService.GetProgramCount());
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
        public IActionResult DeleteProgram(int id)
        {
            try
            {
                bool result = loyaltyProgramService.DeleteProgram(id);
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
        public IActionResult UpdateProgram([FromBody] Models.Program program, int id)
        {
            try
            {
                bool result = loyaltyProgramService.UpdateProgram(program, id);
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
        public IActionResult AddBrand([FromBody] Models.Program program)
        {
            try
            {
                bool result = loyaltyProgramService.AddProgram(program);
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
