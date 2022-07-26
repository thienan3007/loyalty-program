using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace LoyaltyProgram.Handlers
{
    public class ListVoucherWalletAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, List<VoucherWallet>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, List<VoucherWallet> resource)
        {
            var permissions = new List<Role>();
            var id = int.Parse(context.User.Claims.First(c => c.Type == ("id")).Value);
            if (context.User.HasClaim("role", "Admin") && requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            bool result = true;
            foreach (VoucherWallet voucherWallet in resource)
            {
                if (voucherWallet.MembershipId != id)
                {
                    result = false;
                    break;
                }
            }

            if (context.User.HasClaim("role", "User") && result == true && requirement.Name == Operations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            return Task.CompletedTask;
        }
    }
}
