using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/brands")]
    [ApiVersion("1.0")]
    public class BrandController : Controller
    {
        private BrandService brandService;
        public BrandController(BrandService brandService)
        {
            this.brandService = brandService;
        }


        [Produces("application/json")]
        [HttpGet("get-brands")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(brandService.GetBrands());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
