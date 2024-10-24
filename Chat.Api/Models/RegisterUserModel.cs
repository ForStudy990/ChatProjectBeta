using System.ComponentModel.DataAnnotations;
using Chat.Api.Entities;

namespace Chat.Api.Models;

public class RegisterUserModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfrmPassword{ get; set; }
    
}