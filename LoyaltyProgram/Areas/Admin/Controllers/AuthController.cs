using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : Controller
    {
        private readonly AuthService authService;
        private readonly IConfiguration configuration;
        private readonly MembershipService membershipService;

        public AuthController(AuthService authService, IConfiguration configuration, MembershipService membershipService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.membershipService = membershipService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] string idToken)
        {
            try
            {
                var stream = idToken;
                var handler = new JwtSecurityTokenHandler();
                var email = handler.ReadJwtToken(stream).Payload.Values.ToList()[9].ToString();

                if (email != null)
                {
                    var membership = authService.Login(email);
                    if (membership != null)
                    {
                        var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                        var token = CreateToken(authClaims);

                        var refreshToken = GenerateRefreshToken();

                        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                        var result = authService.UpdateRefreshToken(email, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                        if (result == true)
                        {
                            return Ok(new
                            {
                                Token = new JwtSecurityTokenHandler().WriteToken(token),
                                RefreshToken = refreshToken,
                                Expiration = token.ValidTo
                            });
                        }
                    }
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpPost("register")]
        public IActionResult Register([FromBody] string idToken)
        {
            try
            {
                var stream = idToken;
                var handler = new JwtSecurityTokenHandler();
                var email = handler.ReadJwtToken(stream).Payload.Values.ToList()[9].ToString();

                if (email != null)
                {
                    var result = authService.CheckExisted(email);
                    if (result == true)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                    Membership membership = new Membership();
                    membership.Email = email;
                    var username = email.Split("@")[0];
                    //need to change the value of loyaltyprogramId
                    membership.LoyaltyProgramId = 1;
                    membership.EnrollmenDate = DateTime.Now;
                    membership.CanReceivePromotions = true;
                    membership.LastTransactionDate = null;
                    membership.MembershipEndDate = DateTime.Now.AddYears(1);
                    membership.MembershipCode = "CODEMEMBERSHIP" + username;
                    membership.ReferrerMemberId = null;
                    membership.ReferrerMemberDate = null;
                    membership.Status = 1;
                    membership.Description = "";

                    membershipService.AddMembership(membership);

                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest();
            } catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string email = principal.Claims.ElementAt(0).Value;

            var membership = authService.GetMembership(email);
            
            if (membership == null || membership.RefreshToken != refreshToken || membership.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            var result = authService.UpdateRefreshToken(email, newRefreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));
            
            if (result == true)
            {
                return new ObjectResult(new
                {
                    accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    refreshToken = newRefreshToken
                });
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("revoke/{email}")]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public IActionResult Revoke(string email)
        {
            var membership = authService.GetMembership(email);
            if (membership == null) return BadRequest("Invalid email");

            authService.RemoveRefreshToken(email);

            return NoContent();
        }

        [Authorize]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        [HttpPost("revoke-all")]
        public IActionResult RevokeAll()
        {
            try
            {
                authService.RevokeAll();
                return NoContent();
            } catch
            {
                return BadRequest();
            }
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;


        }
    }
}
