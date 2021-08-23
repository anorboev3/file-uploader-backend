using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        public async Task<IActionResult> Uptade(SettingsRequestModel settingsRequestModel)
        {
            await _settingsService.Update(settingsRequestModel);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            return Ok(await _settingsService.Get());
        }

        [HttpGet]
        [Route("extensions")]
        public IActionResult GetExtensions()
        {
            return Ok(_settingsService.GetFileExtensions());
        }

    }
}
