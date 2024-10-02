using ChatProject.Entities;
using ChatProject.Exceptions;
using ChatProject.Managers;
using ChatProject.Models;
using ChatProject.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("Index")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("get-user-by-id")]
    public async Task<IActionResult> GetUserById([FromBody] Guid userId)
    {
        try
        {
            var user = await _userManager.GetUserById(userId);
            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
    {
        var result = await _userManager.Register(model);
        return Ok(result);
    }
    
    //login
    
}