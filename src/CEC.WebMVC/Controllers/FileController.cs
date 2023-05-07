using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> UploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Json(new { success = true, filePath = @$"upload/{fileName}" });
            }
            return Json(new { success = false, filePath = string.Empty });
        }

        [HttpPost]
        public ActionResult UploadFiles(List<IFormFile> files)
        {
            return Ok();
        }
    }
}