using LoyaltyProgram.Auth;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IConfiguration configuration;
        //private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _next = next;
            _appSettings = appSettings.Value;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext, AdminService adminService, MembershipService membershipService)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(httpContext, token, adminService, membershipService);
            await _next(httpContext);
        }

        private void attachUserToContext(HttpContext context, string token, AdminService adminService, MembershipService membershipService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    //ClockSkew = TimeSpan.Zero
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = jwtToken.Claims.First(x => x.Type == "email").Value;

                //Debug.WriteLine("---------------------------------------0" );
                //Debug.WriteLine("email: " + email);
                //Debug.WriteLine("---------------------------------------0");
                //check that email is admin or user 
                //check admin 
                bool resultAdmin = adminService.GetByEmail(email);

                if (resultAdmin)
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = new ApplicationUser() { Email = email, Role = Role.Admin };
                }
                else
                {
                    var resultUser = membershipService.GetMembership(email);

                    if (resultUser != null)
                    {
                        // attach user to context on successful jwt validation
                        context.Items["User"] = new ApplicationUser() { Email = email, Role = Role.User, Id = resultUser.AccountId };
                    }
                }

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
