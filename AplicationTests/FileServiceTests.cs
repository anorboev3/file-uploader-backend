using Application.Services;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AplicationTests
{
    public class FileServiceTests
    {
        private readonly ApplicationContext _aplicationContext;
        private readonly FileService _fileServiceTests;

        public FileServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"{DateTime.Now.ToFileTime()}")
                .Options;

            _aplicationContext = new ApplicationContext(options);
            _aplicationContext.Files.AddRange(new List<File>
            {
                new File { Id = 1, 
                    FileName = "cv.pdf", 
                    FileSystemName = "132740076794159216_cv.pdf", 
                    FileSize = 119448,
                    FileExtension = "pdf",
                    UploadDate = new DateTime(2021, 8, 21, 01, 02, 03),

                },
                new File { 
                    Id = 2, 
                    FileName = "image1.png", 
                    FileSystemName = "132740195449485586_image1.png", 
                    FileSize = 78207,
                    FileExtension = "png",
                    UploadDate = new DateTime(2021, 8, 21, 03, 02, 01) 
                },
                new File {
                    Id = 3,
                    FileName = "image2.png",
                    FileSystemName = "132740195449486654_image2.png",
                    FileSize = 78207,
                    FileExtension = "png",
                    UploadDate = new DateTime(2021, 8, 21, 03, 02, 01)
                },
            });
            _aplicationContext.SaveChanges();

            _fileServiceTests = new FileService(_aplicationContext);
        }

        [Fact]
        public async void GetAll_ReturnsAllFilesList()
        {
            var result = await _fileServiceTests.GetAll();

            Assert.True(2 == result.Count);
            Assert.Equal("pdf", result[0].FileExtension);
            Assert.Equal("png", result[1].FileExtension);
            Assert.True(1 == result[0].Files.ToList().Count);
            Assert.True(2 == result[1].Files.ToList().Count);
            Assert.Equal("cv.pdf", result[0].Files.ToList()[0].FileName);
            Assert.Equal(119448, result[0].Files.ToList()[0].FileSize);
            Assert.Equal(new DateTime(2021, 8, 21, 01, 02, 03), result[0].Files.ToList()[0].UploadDate);
        }

        [Fact]
        public async void GetDownloadInfo_ReturnsInfoForDownloading()
        {
            var result = await _fileServiceTests.GetDownloadInfo(1);
            var dbFile = await _aplicationContext.Files.FirstOrDefaultAsync(x => x.Id == 1);

            Assert.Equal(dbFile.FileName, result.FileName);
            Assert.Equal("application/pdf", result.FileType);
            Assert.Equal(System.IO.Path.GetFullPath($"./Files/{dbFile.FileSystemName}"), result.FilePath);
        }

        [Fact]
        public async void GetDownloadInfo_ThrowsExeption()
        {
            var result = await Assert.ThrowsAsync<Exception>(async () => await _fileServiceTests.GetDownloadInfo(0));

            Assert.Equal("File not found", result.Message);
        }
    }
}
