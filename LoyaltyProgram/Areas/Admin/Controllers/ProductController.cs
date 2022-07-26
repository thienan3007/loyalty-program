using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize(Role.Admin)]
    [Route("api/v{version:apiVersion}/product")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("")]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(this.productService.FindAll());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
