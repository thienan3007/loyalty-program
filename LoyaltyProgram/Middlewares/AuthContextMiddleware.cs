using LoyaltyProgram.Areas.Admin;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoyaltyProgram.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthContextMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // in middleware, services injected in the constructor need to be singletons, since middleware is itself a singleton. Thus if you need per request scoped dependencies, you can take them as parameters to InvokeAsync and ASP.NET will resolve them for you
        public async Task InvokeAsync(HttpContext httpContext, DatabaseContext databaseContext )
        {
            Debug.WriteLine("-----------------------------");
            Debug.WriteLine("user: " + httpContext.User);
            Debug.WriteLine("-----------------------------");
            if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
            {
                // user is not authenticated, so run the next middleware in the piplline and bail out 
                await _next(httpContext);
                return;
            }

            List<Claim> claims = new();

            //get the user record from the database. Presumably the JWT contains a claim that we can use to look up the row 
            

            //conver the user's role to a list of permissions 
            IEnumerable<Permission> permissions = PermissionMap.GetPermissions(Auth.Role.Admin);

            //convert the list of permissions into Claims
            claims.AddRange(permissions.Select(p => new Claim("permission", p.ToString())));

            //enhance the request's current user with the new claims
            ClaimsIdentity enhancedIdentity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(enhancedIdentity);

            // run the next middleware in the pipeline
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthContextMiddleware>();
        }
    }
}
