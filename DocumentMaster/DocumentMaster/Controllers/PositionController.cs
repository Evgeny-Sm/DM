using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PositionController:ControllerBase
    {
        private readonly PositionService _positionService;
        public PositionController(PositionService positionService)
        {
            _positionService = positionService;
        }
        // GET: api/Position
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _positionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Position/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemById(int id)
        {
            var result = await _positionService.GetPositiontByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Position/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem([FromRoute] int id, [FromBody] PositionDTO unit)
        {
            try
            {
                await _positionService.UpdateAsync(id, unit);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
            return NoContent();

        }

        // POST: api/Position
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> PostItem(PositionDTO unit)
        {
            try
            {
                var result = await _positionService.AddPositionAsync(unit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
        }

        // DELETE: api/Position/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                var result = await _positionService.Delete(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }

        }

    }
}
