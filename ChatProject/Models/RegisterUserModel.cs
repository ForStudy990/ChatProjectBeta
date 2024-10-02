using System.ComponentModel.DataAnnotations;
using ChatProject.Entities;

namespace ChatProject.Models;

public class RegisterUserModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string Password { get; set; }


}