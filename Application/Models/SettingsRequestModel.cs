using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class SettingsRequestModel
    {
        [Required]
        public string FileExtensions { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public float FileSize { get; set; }
    }
}
