using System.Collections.Generic;

namespace Application.Models
{
    public class FilesListViewModel
    {
        public string FileExtension { get; set; }

        public IEnumerable<FileViewModel> Files { get; set; }
    }
}
