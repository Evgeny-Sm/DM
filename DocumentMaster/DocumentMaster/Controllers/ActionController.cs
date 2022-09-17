using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionController:ControllerBase
    {
        private readonly ActionService _actionService;
        public ActionController(ActionService actionService)
        { 
            _actionService = actionService;
        }
        // GET: api/Action
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _actionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Action/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetActionAsync(int id)
        {
            var result = await _actionService.GetActionByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // GET: api/Action/ActionCode?number=number
        [HttpGet("ActionCode")]
        public async Task<ActionResult> GetActionsByNumberAsync([FromQuery]int number)
        {
            var result = await _actionService.GetAllActionsByNumberAsync(number);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // GET: api/Action/ActionOndate?dateFrom=date1&dateTo=date2
        [Authorize(Roles = "admin")]
        [HttpGet("ActionOndate")]
        public async Task<ActionResult> GetActionsByDate([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            var result = await _actionService.GetAllActionsOnDateAsync(dateFrom, dateTo);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _actionService.Delete(id);
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
