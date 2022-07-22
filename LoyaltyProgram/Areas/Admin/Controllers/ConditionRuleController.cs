using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/condition-rules")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class ConditionRuleController : Controller
    {
        private ConditionRuleService conditionRuleService;
        public ConditionRuleController(ConditionRuleService conditionRuleService)
        {
            this.conditionRuleService = conditionRuleService;   
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
                return Ok(conditionRuleService.GetConditionRules(pagingParameters));
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
                return Ok(conditionRuleService.FindAll());
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
                return Ok(conditionRuleService.GetConditionRule(id));
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
                return Ok(conditionRuleService.GetCount());
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
                bool result = conditionRuleService.Delete(id);
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
        public IActionResult Update([FromBody] ConditionRule conditionRule, int id)
        {
            try
            {
                bool result = conditionRuleService.Update(conditionRule, id);
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
        public IActionResult Add([FromBody] ConditionRule conditionRule)
        {
            try
            {
                bool result = conditionRuleService.Add(conditionRule);
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
