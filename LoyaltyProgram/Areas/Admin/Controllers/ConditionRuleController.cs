using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/condition-rules")]
    [ApiVersion("1.0")]
    [ApiController]
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
        public IActionResult FindAll()
        {
            try
            {
                return Ok(conditionRuleService.GetConditionRules());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
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
