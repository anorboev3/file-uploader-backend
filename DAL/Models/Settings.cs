using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileExtensions { get; set; }
        [Required]
        public long FileSizeInBytes { get; set; }
        [Required]
        public float FileSizeInMegaBytes { get; set; }
    }
}
