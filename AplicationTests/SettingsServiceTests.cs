using Application.Models;
using Application.Services;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AplicationTests
{
    public class SettingsServiceTests
    {
        private readonly ApplicationContext _aplicationContext;
        private readonly SettingsService _settingsServiceTests;

        public SettingsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"{DateTime.Now.ToFileTime()}")
                .Options;

            _aplicationContext = new ApplicationContext(options);
            _aplicationContext.Settings.Add(new Settings { 
                Id = 1,
                FileExtensions = "test",
                FileSizeInBytes = 8388608,
                FileSizeInMegaBytes = 8
            });
            _aplicationContext.SaveChanges();

            _settingsServiceTests = new SettingsService(_aplicationContext, null);
        }

        [Fact]
        public async void Get_ReturnsSettingsViewModel()
        {
            var result = await _settingsServiceTests.Get();

            Assert.Equal(8, result.FileSizeInMegaBytes);
            Assert.Equal(8388608, result.FileSizeInBytes);
            Assert.Equal("test", result.FileExtensions);
        }

        [Fact]
        public async void Update_UpdatesSettings()
        {
            var result = _settingsServiceTests
                .Update(GetTestRequestModel());
            var Updatedresult = await _settingsServiceTests.Get();

            Assert.True(result.IsCompleted);
            Assert.Equal(5, Updatedresult.FileSizeInMegaBytes);
            Assert.Equal(5242880, Updatedresult.FileSizeInBytes);
            Assert.Equal("updatedTest", Updatedresult.FileExtensions);
        }

        private SettingsRequestModel GetTestRequestModel()
        {
            return new SettingsRequestModel
            {
                FileSize = 5,
                FileExtensions = "updatedTest"
            };
        }
    }
}
