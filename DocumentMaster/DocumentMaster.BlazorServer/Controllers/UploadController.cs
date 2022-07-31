using Microsoft.AspNetCore.Mvc;

namespace DocumentMaster.BlazorServer.Controllers
{

    [DisableRequestSizeLimit]
    public class UploadController:Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
           _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("upload/single")]
        public IActionResult Single(IFormFile file)
        {
            try
            {
                UploadFile(file);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async void UploadFile(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                string path = "\\Upload";
                var uploadPath=_webHostEnvironment.WebRootPath + path;
                if (!Directory.Exists(uploadPath))
                { 
                    Directory.CreateDirectory(uploadPath);
                }
                var fullPath=Path.Combine(uploadPath, file.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create,FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

    }
}
