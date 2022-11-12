using MallManagment.Models.Constants;
using MallManagment.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("getID/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(), FileConstants.GetUserFileDirectory(id));

            FileData fileData = new FileData { UserId = id };

            try
            {
                // get the local filename
                var filePath = Path.Combine(userDirPath, FileConstants.USER_ID);

                // if file is not exist load default image
                if (System.IO.File.Exists(filePath))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    fileData.FileBase64data = Convert.ToBase64String(memory.ToArray());
                    fileData.DataType = Path.GetExtension(filePath);
                }
                else
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), FileConstants.DEFAULT_PROFILE_IMAGE);
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    fileData.FileBase64data = Convert.ToBase64String(memory.ToArray());
                    fileData.DataType = Path.GetExtension(filePath);
                }
                // open for writing

                return Ok(fileData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("uploadID")]
        public async Task<IActionResult> UploadUserID(FileData fileData)
        {
            if (fileData == null || fileData.FileBase64data!.Length == 0)
            {
                return BadRequest();
            }

            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(),
                FileConstants.GetUserFileDirectory(fileData.UserId));

            DirectoryInfo di = new DirectoryInfo(userDirPath);
            if (!di.Exists)
                di.Create();

            try
            {
                // get the local filename
                var filePath = Path.Combine(userDirPath, FileConstants.USER_ID);

                // delete the file exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                var buf = Convert.FromBase64String(fileData.FileBase64data);
                await System.IO.File.WriteAllBytesAsync(filePath, buf);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
