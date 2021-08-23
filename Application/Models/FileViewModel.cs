using System;

namespace Application.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public float FileSize { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
