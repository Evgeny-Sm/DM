using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.BlazorServer.Controllers
{
    [DisableRequestSizeLimit]
    public partial class UploadController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public UploadController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;

            _configuration = configuration;
        }
        [HttpPost("upload")]
     
        public async Task<IActionResult> Single(IFormFile file,[FromQuery]int id,[FromQuery] string fileName)
        {
            try
            {
                UploadFile(file, id, fileName);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task UploadFile(IFormFile file, int projectId, string fileName)
        {

            if (file != null && file.Length > 0)
            {
                string path = _configuration.GetConnectionString("ProjectRootDir")+$"\\{projectId}\\";
                var uploadPath=_webHostEnvironment.WebRootPath + path;
                if (!Directory.Exists(uploadPath))
                { 
                    Directory.CreateDirectory(uploadPath);
                }
                var fullPath=Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create,FileAccess.Write))
                {
                    
                    await file.OpenReadStream().CopyToAsync(fileStream);

                }
            }
        }

    }
}
