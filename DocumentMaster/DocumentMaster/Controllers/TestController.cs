using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        private readonly ProjectService _projectService;
        private readonly ActionService _actionService;


        public TestController(DepartmentService departmentService, ProjectService projectService, ActionService actionService)
        { 
            _departmentService = departmentService;
            _projectService = projectService;
            _actionService = actionService;
        }

        // GET: api/Test
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _actionService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUnit(int id)
        {
            var result = await _actionService.GetActionByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


    }
}
