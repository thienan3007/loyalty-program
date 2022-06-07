using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/currencies")]
    [ApiVersion("1.0")]
    public class CurrencyController : Controller
    {
        private CurrencyService currencyService;
        public CurrencyController(CurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(currencyService.GetCurrencies());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetCurrency(int id)
        {
            try
            {
                return Ok(currencyService.GetCurrencyById(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("count")]
        public IActionResult GetCoount()
        {
            try
            {
                return Ok(currencyService.GetCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            try
            {
                bool result = currencyService.DeleteCurency(id);
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
        [HttpPut("{id}")]
        public IActionResult UpdateCurrency([FromBody] Currency currency, int id)
        {
            try
            {
                bool result = currencyService.UpdateCurency(currency, id);
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
        public IActionResult AddCurrency([FromBody] Currency currency)
        {
            try
            {
                bool result = currencyService.AddCurrency(currency);
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
