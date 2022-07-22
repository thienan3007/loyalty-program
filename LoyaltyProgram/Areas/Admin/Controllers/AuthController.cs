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
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;

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
        private readonly MemberTierService memberTierService;
        private readonly MemberCurrencyService memberCurrencyService;
        private readonly AdminService adminService;
        private readonly TierService tierService;
        private readonly CardService cardService;
        private readonly DeviceService deviceService;

        public AuthController(AuthService authService, IConfiguration configuration, MembershipService membershipService, MemberTierService memberTierService, MemberCurrencyService memberCurrencyService, CardService cardService, TierService tierService, AdminService adminService, DeviceService deviceService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.membershipService = membershipService;
            this.memberTierService = memberTierService;
            this.memberCurrencyService = memberCurrencyService;
            this.cardService = cardService;
            this.tierService = tierService;
            this.adminService = adminService;
            this.deviceService = deviceService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("logout")]
        [Produces("application/json")]
        [Authorize(Role.User)]
        public IActionResult Logout(AuthObject authObject)
        {
            try
            {
                var applictionUser = (ApplicationUser) HttpContext.Items["User"];
                var device = new Device();
                device.AccountId = applictionUser.Id;
                if (authObject.DeviceId != null)
                {
                    device.DeviceId = authObject.DeviceId;
                    var result = this.deviceService.Remove(device);
                    if (result)
                    {
                        return Ok("successful");
                    }
                }

                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] AuthObject authObject)
        {
            try
            {
                var stream = authObject.IdToken;
                var handler = new JwtSecurityTokenHandler();
                var email = handler.ReadJwtToken(stream).Claims.First(claim => claim.Type == "email").Value;

                if (email != null)
                {
                    //check admin or user 
                    var resultAdmin = this.adminService.GetByEmail(email);
                    if (resultAdmin)
                    {
                        var authClaims = new List<Claim>
                            {
                                new Claim("email", email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("role", "admin")
                            };

                        var token = CreateToken(authClaims);

                        //var refreshToken = GenerateRefreshToken();

                        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                        //var result = authService.UpdateRefreshToken(email, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            //RefreshToken = refreshToken,
                            Expiration = token.ValidTo
                        });
                    }
                    else
                    {
                        var membership = authService.Login(email);
                        var membershipId = membership.AccountId;

                        //get current membertier of this membership
                        var memberTier = this.memberTierService.GetMemberTierCurrent(membershipId);
                        var tier = this.tierService.GetTierByID(memberTier.LoyaltyTierId).Name;
                        //get point 
                        var point = this.memberCurrencyService.GetMembershipCurrency(membershipId, 2).PointsBalance;
                        if (membership != null)
                        {
                            var authClaims = new List<Claim>
                            {
                                new Claim("email", email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("role", "user")
                            };

                            var token = CreateToken(authClaims);

                            var refreshToken = GenerateRefreshToken();

                            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                            var result = authService.UpdateRefreshToken(email, refreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));

                            if (result == true)
                            {
                                //regis device for this account 
                                if (authObject.DeviceId != null)
                                {
                                    var device = new Device();
                                    device.AccountId = membershipId;
                                    device.DeviceId = authObject.DeviceId;
                                    var ok = this.deviceService.Add(device);
                                    if (ok)
                                    {
                                        return Ok(new
                                        {
                                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                                            RefreshToken = refreshToken,
                                            Expiration = token.ValidTo.ToString(),
                                            Now = DateTime.Now.ToString(),
                                            AccountId = membership.AccountId,
                                            Point = point,
                                            Tier = tier,
                                        });
                                    }
                                }
                                
                            }
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
        public IActionResult Register([FromBody] AuthObject authObject)
        {
            try
            {
                var stream = authObject.IdToken;
                var handler = new JwtSecurityTokenHandler();
                var email = handler.ReadJwtToken(stream).Claims.First(claim => claim.Type == "email").Value;
                var name = handler.ReadJwtToken(stream).Claims.First(claim => claim.Type == "name").Value;

                if (email != null)
                {
                    var result = authService.CheckExisted(email);
                    if (result == true)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                    Membership membership = new Membership();
                    membership.Email = email;
                    var username = email.Split("@")[0];

                    //add to membership
                    //need to change the value of loyaltyprogramId
                    membership.LoyaltyProgramId = 1;
                    membership.EnrollmentDate = DateTime.Now;
                    membership.CanReceivePromotions = true;
                    membership.LastTransactionDate = null;
                    membership.MembershipEndDate = DateTime.Now.AddYears(1);
                    membership.MembershipCode = "CODEMEMBERSHIP" + username;
                    membership.ReferrerMemberId = null;
                    membership.ReferrerMemberDate = null;
                    membership.Status = 1;
                    membership.Description = username + " joined us on " + membership.EnrollmentDate;
                    int id = membershipService.AddMembership(membership);

                    //add to membertier
                    //need to check loyaltyProgramID to input data for membertier
                    MemberTier memberTier = new MemberTier();
                    memberTier.EffectiveDate = DateTime.Now;
                    memberTier.UpdateTierDate = DateTime.Now;
                    memberTier.LoyaltyTierId = 1;
                    memberTier.LoyaltyMemberId = id;
                    memberTier.Status = 1;
                    memberTier.Description = "tier 1";
                    memberTier.Name = "Tier 1";
                    memberTier.ExpirationDate = DateTime.Now.AddMonths(3);
                    this.memberTierService.AddMemberTier(memberTier);

                    //add to memberCurrency

                    //money
                    MembershipCurrency membershipCurrency = new MembershipCurrency();
                    membershipCurrency.CurrencyId = 2;
                    membershipCurrency.Name = "Point";
                    membershipCurrency.PointsBalance = 0;
                    membershipCurrency.TotalPointsAccrued = 0;
                    membershipCurrency.TotalPointsExpired = 0;
                    membershipCurrency.TotalPointsRedeemed = 0;
                    membershipCurrency.BalanceBeforeReset = 0;
                    membershipCurrency.LastResetDate = DateTime.Now;
                    membershipCurrency.ExpirationPoints = 0;
                    membershipCurrency.MembershipId = id;
                    membershipCurrency.Status = 1;
                    membershipCurrency.Description = "Point";

                    //point
                    MembershipCurrency membershipCurrency1 = new MembershipCurrency();
                    membershipCurrency1.CurrencyId = 5;
                    membershipCurrency1.Name = "Money";
                    membershipCurrency1.PointsBalance = 0;
                    membershipCurrency1.TotalPointsAccrued = 0;
                    membershipCurrency1.TotalPointsExpired = 0;
                    membershipCurrency1.TotalPointsRedeemed = 0;
                    membershipCurrency1.BalanceBeforeReset = 0;
                    membershipCurrency1.LastResetDate = DateTime.Now;
                    membershipCurrency1.ExpirationPoints = 0;
                    membershipCurrency1.MembershipId = id;
                    membershipCurrency1.Status = 1;
                    membershipCurrency1.Description = "Money";

                    //add memberCurrency
                    this.memberCurrencyService.AddMembershipCurrency(membershipCurrency);
                    this.memberCurrencyService.AddMembershipCurrency(membershipCurrency1);

                    //add to Card
                    Card card = new Card();
                    card.Type = "loyalty card";
                    card.Discount = 0;
                    card.CardholderName = name;
                    card.MembershipId = id;
                    card.BrandId = 1;
                    card.Status = 1;
                    card.CurrencyId = 1;
                    card.Amount = 0;
                    card.CreatedAt = DateTime.Now;
                    card.Description = "card";

                    //add card
                    this.cardService.Add(card);

                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                return BadRequest();
            }
            catch
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

            string email = principal.Claims.First(claim => claim.Type == "email").Value;

            var membership = authService.GetMembership(email);

            if (membership == null || membership.RefreshToken != refreshToken || membership.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            //var newRefreshToken = GenerateRefreshToken();

            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            //var result = authService.UpdateRefreshToken(email, newRefreshToken, DateTime.Now.AddDays(refreshTokenValidityInDays));


            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                //refreshToken = newRefreshToken
            });

        }

        [Authorize(Role.Admin)]
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

        [Authorize(Role.Admin)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        [HttpPost("revoke-all")]
        public IActionResult RevokeAll()
        {
            try
            {
                authService.RevokeAll();
                return NoContent();
            }
            catch
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
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private JwtSecurityToken GetPrincipalFromExpiredToken(string? token)
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
            var jwtToken = (JwtSecurityToken)securityToken;
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return jwtToken;
        }
    }
}
