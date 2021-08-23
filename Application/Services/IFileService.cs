using Application.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IFileService
    {
        public Task Upload(IFormFile file);
        public Task<List<FilesListViewModel>> GetAll();
        public Task<FileDownloadInfoViewModel> GetDownloadInfo(int id);
    }
}
