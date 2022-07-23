using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController:ControllerBase
    {
        private readonly FileService _fileService;
        public FileController(FileService fileService)
        { 
            _fileService = fileService;
        }
        // GET: api/File
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _fileService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/File
        [Authorize]
        [HttpGet("NotDeleted")]
        public async Task<ActionResult> GetAllNotDeleted()
        {
            var result = await _fileService.GetAllNotDeletedAsync();
            return Ok(result);
        }

        // GET: api/File/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFile(int id)
        {
            var result = await _fileService.GetItemByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}
