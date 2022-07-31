using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController:ControllerBase
    {
        private readonly FileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileController(FileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
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

        
        // POST: api/File/1
        [Authorize]
        [HttpPost("{personId}")]
        public async Task<ActionResult> AddFileAsync([FromBody] FileDTO fileDTO, int personId)
        {
            try
            {
                var result = await _fileService.AddFileAsync(fileDTO, personId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception {ex.Message}");
            }
        }

        // POST: api/File/Upload?fileUnitId=1
        [Authorize]
        [HttpPost("Upload/{id}")]

        public async Task<ActionResult> UploadFile(IFormFile file,int id)
        {
            if (file != null)
            {
                
                var f = await _fileService.GetItemByIdAsync(id);

                string path = $"/{f.ProjectId}/" + file.FileName;
              
                // сохраняем файл в папку с проектом в каталоге wwwroot
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok();
            }
            return BadRequest();
        
        }



    }
}
