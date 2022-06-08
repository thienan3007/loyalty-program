using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/voucher-wallets")]
    [ApiVersion("1.0")]
    public class VoucherWalletController : Controller
    {
        private VoucherWalletService voucherWalletService;
        public VoucherWalletController(VoucherWalletService voucherWalletService)
        {
            this.voucherWalletService = voucherWalletService; 
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(voucherWalletService.GetVoucherWallets());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{membershipId}/{voucherDefinitionId}")]
        public IActionResult Get(int membershipId, int voucherDefinitionId)
        {
            try
            {
                return Ok(voucherWalletService.GetVoucherWallet(membershipId, voucherDefinitionId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("count")]
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

        [Produces("application/json")]
        [HttpDelete("{membershipId}/{voucherDefinitionId}")]
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

        [Produces("application/json")]
        [HttpPut("{membershipId}/{voucherDefinitionId}")]
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

        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult Add([FromBody] VoucherWallet voucherWallet)
        {
            try
            {
                bool result = voucherWalletService.Add(voucherWallet);
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
