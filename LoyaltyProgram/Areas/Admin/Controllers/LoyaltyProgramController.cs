using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Areas.Admin.Controllers
{
    [Route("api/v{version:apiVersion}/programs")]
    [ApiVersion("1.0")]
    public class LoyaltyProgramController : Controller
    {
        private LoyaltyProgramService loyaltyProgramService;
        public LoyaltyProgramController(LoyaltyProgramService loyaltyProgramService)
        {
            this.loyaltyProgramService = loyaltyProgramService;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(loyaltyProgramService.GetPrograms());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetProgram(int id)
        {
            try
            {
                return Ok(loyaltyProgramService.GetProgramById(id));
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
                return Ok(loyaltyProgramService.GetProgramCount());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProgram(int id)
        {
            try
            {
                bool result = loyaltyProgramService.DeleteProgram(id);
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
        public IActionResult UpdateProgram([FromBody] Models.Program program, int id)
        {
            try
            {
                bool result = loyaltyProgramService.UpdateProgram(program, id);
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
        public IActionResult AddBrand([FromBody] Models.Program program)
        {
            try
            {
                bool result = loyaltyProgramService.AddProgram(program);
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
