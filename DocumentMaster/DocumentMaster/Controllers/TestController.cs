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
        private readonly IWebHostEnvironment _appEnvironment;


        public TestController(DepartmentService departmentService, ProjectService projectService, ActionService actionService, IWebHostEnvironment appEnvironment)
        { 
            _departmentService = departmentService;
            _projectService = projectService;
            _actionService = actionService;
            _appEnvironment = appEnvironment;

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
        // GET: api/Test/GetFile
        [HttpGet("GetFile")]
        public IActionResult GetFile()
        {
            /*var filePath = Path.Combine("~/Files", "КМ.dwg");
            var f= File(filePath, "application/octet-stream");
            return Ok(f);*/
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/hello.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "hello.pdf";
            var f = PhysicalFile(file_path, file_type, file_name);
            return f;

        }


    }
}
