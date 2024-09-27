using System.ComponentModel.DataAnnotations;

namespace ChatProject.Models;

public class FileModel
{
    [Required]
    public IFormFile File { get; set; }
    public string? Caption { get; set; }
}