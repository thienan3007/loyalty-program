using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace LoyaltyProgram.Handlers
{
    public class MemberTierAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, MemberTier>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, MemberTier resource)
        {
            var permissions = new List<Role>();
            var id = int.Parse(context.User.Claims.First(c => c.Type == ("id")).Value);
            if (context.User.HasClaim("role", "Admin") && requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            if (context.User.HasClaim("role", "User") && id == resource.LoyaltyMember.AccountId && requirement.Name == Operations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            return Task.CompletedTask;
        }
    }
}
