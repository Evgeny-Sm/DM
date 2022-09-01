using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        { 
            _projectService = projectService;
        }
        // GET: api/Project
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetProjects()
        {
            var result = await _projectService.GetProjectsListAsync();
            return Ok(result);
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProject(int id)
        {
            var result = await _projectService.GetProjectByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Project/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProject([FromRoute] int id, [FromBody] ProjectDTO unit)
        {
            try
            {
                await _projectService.UpdateProjectAsync(id, unit);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
            return NoContent();

        }

        // POST: api/Project
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> PostProject(ProjectDTO unit)
        {
            try
            {
                var result = await _projectService.AddProjectAsync(unit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                var result = await _projectService.RemoveProject(id);
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
