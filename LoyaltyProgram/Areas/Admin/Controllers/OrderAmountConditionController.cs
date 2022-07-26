using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/order-amount-conditions")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(Role.Admin)]
    public class OrderAmountConditionController : Controller
    {
        private OrderAmountConditionService orderAmountConditionService;
        public OrderAmountConditionController(OrderAmountConditionService orderAmountConditionService)
        {
            this.orderAmountConditionService = orderAmountConditionService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy };
                return Ok(orderAmountConditionService.GetOrderAmountConditions(pagingParameters));
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
                return Ok(orderAmountConditionService.GetOrderAmountCondition(id));
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
                return Ok(orderAmountConditionService.GetCount());
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
                bool result = orderAmountConditionService.Delete(id);
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
        public IActionResult Update([FromBody] OrderAmountCondition orderAmountCondition, int id)
        {
            try
            {
                bool result = orderAmountConditionService.Update(orderAmountCondition, id);
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
        public IActionResult Add([FromBody] OrderAmountCondition orderAmountCondition)
        {
            try
            {
                bool result = orderAmountConditionService.Add(orderAmountCondition);
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
