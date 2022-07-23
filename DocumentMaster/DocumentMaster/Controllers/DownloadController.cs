using DM.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DownloadController : ControllerBase
    {
        private readonly FileService _fileService;
        public DownloadController(FileService fileService)
        { 
            _fileService = fileService;
        }

        // GET: api/Download/5
        [HttpGet("{id}")]
        public async Task<VirtualFileResult> DownloadFileAsync(int id)
        {
            var filePath =await _fileService.GetFullFilePathAsync(id);

            var fullPath = Path.Combine("~/", $"{filePath}");

            return File(fullPath, "application/octet-stream");

        }
    }
}
