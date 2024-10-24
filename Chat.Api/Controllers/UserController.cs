using Chat.Api.Exceptions;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Entities;
using Chat.Api.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var result = await _userManager.Login(model);
            return Ok(result);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}