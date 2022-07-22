using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using System.Security.Claims;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Utils;
using Microsoft.AspNetCore.Http.Features;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/voucher-wallets")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class VoucherWalletController : Controller
    {
        private VoucherWalletService voucherWalletService;
        private readonly IAuthorizationService _authorizationService;
        private readonly VoucherDefinitionService voucherDefinitionService;
        private readonly MemberCurrencyService memberCurrencyService;

        private System.Security.Claims.ClaimsPrincipal principal = new ClaimsPrincipal();
        public VoucherWalletController(VoucherWalletService voucherWalletService, IAuthorizationService authorizationService, VoucherDefinitionService voucherDefinitionService, MemberCurrencyService memberCurrencyService)
        {
            this.voucherWalletService = voucherWalletService;
            this._authorizationService = authorizationService;
            this.voucherDefinitionService = voucherDefinitionService;
            this.memberCurrencyService = memberCurrencyService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll(int pageNumber, int pageSize, string? orderBy, string? filterString)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, OrderBy = orderBy, FilterString = filterString };
                return Ok(voucherWalletService.GetVoucherWallets(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{membershipId}/{voucherDefinitionId}")]
        public async Task<IActionResult> Get(int membershipId, int voucherDefinitionId)
        {
            try
            {
                var voucherWallet = voucherWalletService.GetVoucherWallet(membershipId, voucherDefinitionId);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, voucherWallet, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(voucherWallet);
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
        [HttpGet("{membershipId}")]
        public async Task<IActionResult> GetAllVoucher(int membershipId, int pageNumber, int pageSize, string? filterString)
        {
            try
            {

                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString };
                var voucherWallets = voucherWalletService.GetVoucherOfMember(membershipId, pagingParameters);
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, voucherWallets, Operations.Read);
                if (authorizatonResult.Succeeded)
                {
                    return Ok(voucherWallets);
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
        [HttpGet("count")]
        [Authorize(Role.Admin)]
        public IActionResult GetCount()
        {
            try
            {
                return Ok(voucherWalletService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{membershipId}/{voucherDefinitionId}")]
        [Authorize(Role.Admin)]
        public IActionResult Delete(int membershipId, int voucherDefinitionId)
        {
            try
            {
                bool result = voucherWalletService.Delete(membershipId, voucherDefinitionId);
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
        [HttpPut("{membershipId}/{voucherDefinitionId}")]
        [Authorize(Role.Admin)]
        public IActionResult Update([FromBody] VoucherWallet voucherWallet, int membershipId, int voucherDefinitionId)
        {
            try
            {
                bool result = voucherWalletService.Update(voucherWallet, membershipId, voucherDefinitionId);
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
        [HttpPost("present/{memberId}/{voucherId}")]
        [Authorize(Role.Admin)]
        public IActionResult Present(int memberId, int voucherId)
        {
            try
            {
                var voucherWallet = new VoucherWallet();
                var voucher = this.voucherDefinitionService
                    .GetVoucher(voucherId);
                voucherWallet.MembershipId = memberId;
                voucherWallet.VoucherDefinitionId = voucherId;
                voucherWallet.Status = 1;
                voucherWallet.Description = "In stock";
                voucherWallet.IsPartialRedeemable = voucher.IsPartialRedeemable;
                if (voucherWallet.IsPartialRedeemable == true)
                {
                    voucherWallet.RedeemedValue = 0;
                    voucherWallet.RemainingValue = voucher.DiscountValue;
                }

                bool result = voucherWalletService.Add(voucherWallet);
                if (result)
                {
                    return Ok();
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
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] VoucherWallet voucherWallet)
        {
            try
            {
                //get AccountId of a application user 
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaim(new Claim("email", applicationUser.Email));
                identity.AddClaim(new Claim("role", applicationUser.Role == Role.User ? "User" : "Admin"));
                identity.AddClaim(new Claim("id", applicationUser.Id.ToString()));
                principal.AddIdentity(identity);
                var authorizatonResult = await _authorizationService.AuthorizeAsync(principal, voucherWallet, Operations.Read);
                var voucher = this.voucherDefinitionService
                    .GetVoucher(voucherWallet.VoucherDefinitionId);
                if (authorizatonResult.Succeeded && voucher != null)
                {
                    //check user point 
                    var memberCurrency = this.memberCurrencyService.GetMembershipCurrency(voucherWallet.MembershipId, 2);
                    if (memberCurrency != null)
                    {
                        if (memberCurrency.PointsBalance > voucher.Point)
                        {
                            voucherWallet.Status = 1;
                            voucherWallet.Description = "In stock";
                            voucherWallet.IsPartialRedeemable = voucher.IsPartialRedeemable;
                            if (voucherWallet.IsPartialRedeemable == true)
                            {
                                voucherWallet.RedeemedValue = 0;
                                voucherWallet.RemainingValue = voucher.DiscountValue;
                            }

                            bool result = voucherWalletService.Add(voucherWallet);

                            if (result)
                            {
                                //update member point 
                                memberCurrency.PointsBalance = memberCurrency.PointsBalance - voucher.Point;
                                var ok = this.memberCurrencyService.UpdateMembershipCurrency(memberCurrency,
                                    voucherWallet.MembershipId, 2);
                                if (ok)
                                {
                                    return Ok("Successful");
                                }
                            }

                            if (!result)
                            {
                                return BadRequest("Failed");
                            }
                        } //do not have enough point for redeem
                    } //this voucher definition does 
                }

                if (voucher == null)
                {
                    return BadRequest("This voucher Id is not existed");
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
