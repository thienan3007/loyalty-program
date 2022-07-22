using System.Security.Claims;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{

    [Route("api/v{version:apiVersion}/notification")]
    [ApiVersion("1.0")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        private readonly IAuthorizationService _authorizationService;
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService, IAuthorizationService authorizationService)
        {
            _notificationService = notificationService;
            _authorizationService = authorizationService;
        }
        [Route("send")]
        [HttpPost]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [HttpPut("{notificationId}")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public  IActionResult UpdateNotification(int notificationId)
        {
            var result = _notificationService.UpdateNotification(notificationId);
            if (result)
            {
                return Ok("successful");
            }

            return BadRequest();
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public async Task<IActionResult> FindAll(int accountId)
        {
            try
            {
                var notifications = _notificationService.GetAllNotifications(accountId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, notifications, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(notifications);
                }

                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
