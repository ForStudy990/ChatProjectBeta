using System.ComponentModel.DataAnnotations;

namespace ChatProject.Models;

public class TextModel
{
    [Required]
    public string Text { get; set; }
}