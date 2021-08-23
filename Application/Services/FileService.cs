using Application.Models;
using DAL;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationContext _context;
        public FileService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Upload(IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("File is not valid");
            }

            var settings = await _context.Settings.FirstOrDefaultAsync();
            var fileExtensions = Path.GetExtension(file.FileName).TrimStart('.');

            if (!settings.FileExtensions.Contains(fileExtensions))
            {
                throw new Exception($"File with extension {fileExtensions} is not allowed");
            }

            if (settings.FileSizeInBytes < file.Length)
            {
                throw new Exception($"File size is more than allowed");
            }

            try
            {
                var fileSystemName = $"{DateTime.Now.ToFileTime()}_{file.FileName}";
                var filePath = $"./Files/{fileSystemName}";
                using (var stream = File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                var newFile = new DAL.Models.File
                {
                    FileName = file.FileName,
                    FileSize = file.Length,
                    FileSystemName = fileSystemName,
                    UploadDate = DateTime.Now,
                    FileExtension = fileExtensions

                };
                await _context.Files.AddAsync(newFile);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Failed to upload file");
            }
        }

        public async Task<List<FilesListViewModel>> GetAll()
        {
            try
            {
                return (await _context.Files.ToListAsync()).GroupBy(x => x.FileExtension).Select(x => new FilesListViewModel {
                    FileExtension = x.Key,
                    Files = x.Select(x => new FileViewModel { 
                        Id = x.Id,
                        FileName = x.FileName,
                        FileSize = x.FileSize,
                        UploadDate  = x.UploadDate
                    })
                }).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Failed to get data");
            }
        }

        public async Task<FileDownloadInfoViewModel> GetDownloadInfo(int id)
        {
            var dbFile = await _context.Files.FirstOrDefaultAsync(x => x.Id == id);
            if (dbFile == null)
            {
                throw new Exception("File not found");
            }
            return new FileDownloadInfoViewModel
            {
                FilePath = Path.GetFullPath($"./Files/{dbFile.FileSystemName}"),
                FileName = dbFile.FileName,
                FileType = MimeTypes.GetMimeType(dbFile.FileName)
            };
        }
    }
}
