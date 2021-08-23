using Application.Models;
using DAL;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        public SettingsService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Update(SettingsRequestModel settingsRequestModel)
        {
            try
            {
                var dbSettings = await _context.Settings.FirstOrDefaultAsync();
                dbSettings.FileSizeInBytes = (int)settingsRequestModel.FileSize * 1024 * 1024;
                dbSettings.FileSizeInMegaBytes = settingsRequestModel.FileSize;
                dbSettings.FileExtensions = settingsRequestModel.FileExtensions;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Failed to update");
            }
        }

        public async Task<SettingsViewModel> Get()
        {
            try
            {
                var dbSettings = await _context.Settings.FirstOrDefaultAsync();
                return new SettingsViewModel
                {
                    FileSizeInMegaBytes = dbSettings.FileSizeInMegaBytes,
                    FileExtensions = dbSettings.FileExtensions,
                    FileSizeInBytes = dbSettings.FileSizeInBytes
                };
            }
            catch (Exception)
            {
                throw new Exception("Failed to get data");
            }
        }

        public string[] GetFileExtensions()
        {
            return _configuration.GetSection("FileExtensions").Value.Split(",");
        }
    }
}
