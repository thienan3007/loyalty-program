using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/condition-groups")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ConditionGroupController : Controller
    {
        private ConditionGroupService conditionGroupService;
        public ConditionGroupController(ConditionGroupService conditionGroupService)
        {
            this.conditionGroupService = conditionGroupService; 
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll(int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy };    
                return Ok(conditionGroupService.GetConditionGroups(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("find-all")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll()
        {
            try
            {
                var conditionGroupList = conditionGroupService.FindAll();
                if (conditionGroupList != null)
                {
                    return Ok(conditionGroupList);
                }
                return BadRequest();
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
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(conditionGroupService.GetConditionGroup(id));
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
                return Ok(conditionGroupService.GetCount());
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
                bool result = conditionGroupService.Delete(id);
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
        public IActionResult Update([FromBody] ConditionGroup conditionGroup, int id)
        {
            try
            {
                bool result = conditionGroupService.Update(conditionGroup, id);
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
        public IActionResult Add([FromBody] ConditionGroup conditionGroup)
        {
            try
            {
                bool result = conditionGroupService.Add(conditionGroup);
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
