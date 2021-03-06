using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Utils;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/vouchers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class VoucherController : Controller
    {
        private VoucherDefinitionService voucherDefinitionService;
        public VoucherController(VoucherDefinitionService voucherDefinitionService)
        {
            this.voucherDefinitionService = voucherDefinitionService;
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy };
                return Ok(voucherDefinitionService.GetVouchers(pagingParameters));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("user")]
        public IActionResult FindAll()
        {
            try
            {
                var applicationUser = (ApplicationUser)HttpContext.Items["User"];
                return Ok(voucherDefinitionService.GetVouchers(applicationUser.Id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetVoucher(int id)
        {
            try
            {
                return Ok(voucherDefinitionService.GetVoucher(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            try
            {
                return Ok(voucherDefinitionService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpDelete("{id}")]
        [Authorize(Role.Admin)]
        public IActionResult DeleteVoucher(int id)
        {
            try
            {
                bool result = voucherDefinitionService.DeleteVoucher(id);
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
        [HttpPut("{id}")]
        [Authorize(Role.Admin)]
        public IActionResult UpdateVoucher([FromBody] VoucherDefinition voucher, int id)
        {
            try
            {
                bool result = voucherDefinitionService.UpdateVoucher(voucher, id);
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
        [HttpPost("")]
        [Authorize(Role.Admin)]
        public IActionResult AddVoucher([FromBody] VoucherDefinition voucher)
        {
            try
            {
                bool result = voucherDefinitionService.AddVoucher(voucher);
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
