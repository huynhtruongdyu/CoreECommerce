using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var fileSize = file.Length / 1048576.0; //MB;
                    if (fileSize > 3)
                    {
                        return Json(new { success = false, errors = "File size must less than 3MB" });
                    }

                    var fileName = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + Path.GetExtension(file.FileName);
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Json(new { success = true, filePath = @$"upload/{fileName}" });
                }
                return Json(new { success = false, errors = "Error on upload file" });
            }
            catch (Exception)
            {
                return Json(new { success = false, errors = "Error on upload file" });
            }
        }

        [HttpPost]
        public ActionResult UploadFiles(List<IFormFile> files)
        {
            return Ok();
        }
    }
}