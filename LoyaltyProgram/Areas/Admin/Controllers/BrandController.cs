﻿using LoyaltyProgram.Models;
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

        [Produces("application/json")]
        [HttpGet("get-brand/{id}")]
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

        [Produces("application/json")]
        [HttpGet("get-count")]
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

        [Produces("application/json")]
        [HttpGet("delete-brand/{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                bool result = brandService.DeleteBrand(id);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPut("update-brand/{id}")]
        public IActionResult UpdateBrand([FromBody] Brand brand, int id)
        {
            try
            {
                bool result = brandService.UpdateBrand(brand, id);
                return Ok(result ? "Success" : "Failed");   
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("add-brand")]
        public IActionResult AddBrand([FromBody] Brand brand)
        {
            try
            {
                bool result = brandService.AddBrand(brand);
                return Ok(result ? "Successful" : "Failed");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
