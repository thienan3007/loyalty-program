using LoyaltyProgram.Auth;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LoyaltyProgram.Helpers.AuthorizeAttribute;
using LoyaltyProgram.Utils;
using Newtonsoft.Json;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/brands")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BrandController : Controller
    {
        private BrandService brandService;
        public BrandController(BrandService brandService)
        {
            this.brandService = brandService;
        }

        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("")]
        [Authorize(Role.Admin)]
        public IActionResult FindAll(int pageNumber, int pageSize, string? filterString, string? orderBy)
        {
            try
            {
                PagingParameters pagingParameters = new PagingParameters() { PageNumber = pageNumber, PageSize = pageSize, FilterString = filterString, OrderBy = orderBy};
                var brands = brandService.GetBrands(pagingParameters);
                var metadata = new
                {
                    brands.TotalCount,
                    brands.PageSize,
                    brands.CurrentPage,
                    brands.TotalPages,
                    brands.HasNext,
                    brands.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(brands);
            }
            catch
            {
                return BadRequest();
            }
        }
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        [HttpGet("{id}")]
        [Authorize(Role.Admin)]
        public IActionResult GetBrand(int id)
        {
            try
            {
                return Ok(brandService.GetBrandById(id));
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
        public IActionResult GetCoount()
        {
            try
            {
                return Ok(brandService.GetBrandCount());
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
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                bool result = brandService.DeleteBrand(id);
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
        public IActionResult UpdateBrand([FromBody] Brand brand, int id)
        {
            try
            {
                bool result = brandService.UpdateBrand(brand, id);
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
        public IActionResult AddBrand([FromBody] Brand brand)
        {
            try
            {
                bool result = brandService.AddBrand(brand);
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
