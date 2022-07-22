using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace LoyaltyProgram.Handlers
{
    public class MembershipAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Membership>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Membership resource)
        {
            var permissions = new List<Role>();
            var email = context.User.Claims.First(c => c.Type == ("email")).Value;
            //var referrerId = Int.TryParse(context.User.Claims.First(c => c.Type == ("referrer")).Value);
            if (context.User.HasClaim("role", "Admin") && requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            if (context.User.HasClaim("role", "User") && email == resource.Email && requirement.Name == Operations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            return Task.CompletedTask;
        }
    }
}
