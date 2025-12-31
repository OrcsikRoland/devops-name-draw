using Microsoft.AspNetCore.Mvc;
using NameDraw.Api.Dtos;
using NameDraw.Api.Services;

namespace NameDraw.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NamesController : ControllerBase
    {
        private readonly INameService _service;

        public NamesController(INameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<NameDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Add(CreateNameRequestDto req)
        {
            try
            {
                await _service.AddAsync(req.Value);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("random")]
        public async Task<ActionResult<NameDto>> Random()
        {
            var picked = await _service.GetRandomAsync();
            return picked == null ? NoContent() : Ok(picked);
        }
    }
}
