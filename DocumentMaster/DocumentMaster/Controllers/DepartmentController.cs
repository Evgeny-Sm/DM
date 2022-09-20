using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController: ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        // GET: api/Department
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            var result = await _departmentService.GetDepartmentsListAsync();
            return Ok(result);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(int id)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Department/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment( [FromBody] DepartmentDTO unit)
        {
            try
            {
                await _departmentService.UpdateDepartmentAsync( unit);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
            return NoContent();

        }

        // POST: api/Department
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> PostDepartment(DepartmentDTO unit)
        {
            try
            {
                var result = await _departmentService.AddDepartmentAsync(unit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartment(id);
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
