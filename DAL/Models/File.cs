using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(155)]
        public string FileName { get; set; }
        [Required, MaxLength(255)]
        public string FileSystemName { get; set; }
        [Required]
        public float FileSize { get; set; }
        [Required]
        public string FileExtension { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
    }
}
