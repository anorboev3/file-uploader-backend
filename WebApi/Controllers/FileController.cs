using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fileService.GetAll());
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await _fileService.Upload(file);
            return Ok();
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var downloadInfo = await _fileService.GetDownloadInfo(id);
            return PhysicalFile(downloadInfo.FilePath, downloadInfo.FileType, downloadInfo.FileName);
        }
    }
}
