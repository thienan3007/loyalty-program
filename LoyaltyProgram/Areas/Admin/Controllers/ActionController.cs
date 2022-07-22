using LoyaltyProgram.Auth;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using LoyaltyProgram.Utils;
using Microsoft.AspNetCore.Mvc;
using Action = LoyaltyProgram.Models.Action;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/v{version:apiVersion}/actions")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ActionController : Controller
    {
        private ActionService actionService;
        public ActionController(ActionService actionService)
        {
            this.actionService = actionService;
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters()
                    {PageNumber = pageNumber, PageSize = pageSize};
                var actions = actionService.GetActions(pagingParameters);
                if (actions != null)
                {
                    return Ok(actions);
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
        public IActionResult GetAction(int id)
        {
            try
            {
                return Ok(actionService.GetAction(id));
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
                return Ok(actionService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteAction(int id)
        {
            try
            {
                bool result = actionService.DeleteAction(id);
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
        public IActionResult UpdateAction([FromBody] Models.Action action, int id)
        {
            try
            {
                bool result = actionService.UpdateAction(action, id);
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
        public IActionResult Add([FromBody] Models.Action action)
        {
            try
            {
                bool result = actionService.AddAction(action);
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
