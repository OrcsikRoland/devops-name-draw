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
        {
            var sid = Program.GetOrCreateSessionId(HttpContext);
            return Ok(await _service.GetAllAsync(sid));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateNameRequestDto req)
        {
            var sid = Program.GetOrCreateSessionId(HttpContext);

            try
            {
                await _service.AddAsync(sid, req.Value);
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
            var sid = Program.GetOrCreateSessionId(HttpContext);
            await _service.DeleteAsync(sid, id);
            return NoContent();
        }

        [HttpGet("random")]
        public async Task<ActionResult<NameDto>> Random()
        {
            var sid = Program.GetOrCreateSessionId(HttpContext);
            var picked = await _service.GetRandomAsync(sid);
            return picked == null ? NoContent() : Ok(picked);
        }
    }
}
