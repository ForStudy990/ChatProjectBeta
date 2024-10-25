using Chat.Api.Managers;
using Chat.Api.Entities;
using Chat.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : Controller
{
    private readonly ChatManager _manager;
    private readonly UserHelper _userHelper;

    public ChatController(ChatManager manager, UserHelper userHelper)
    {
        _manager = manager;
        _userHelper = userHelper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllChats()
    {
        var chats = await _manager.GetAllChats();
        return Ok(chats);
    }

    [HttpGet("/api/users/{userId}/chats")]
    public async Task<IActionResult> GetUserChats([FromRoute] Guid userId)
    {
        var chats = await _manager.GetAllUserChats(userId);
        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEnterChat([FromBody] Guid reciverId)
    {
        var senderId = _userHelper.GetUserId();
        var chat = await _manager.AddOrEnterChat(reciverId, senderId);
        return Ok(chat);
    }

    [HttpDelete("{chatId:Guid}")]
    public async Task<IActionResult> DeleteChat([FromRoute] Guid chatId)
    {
        try
        {
            var senderId = _userHelper.GetUserId();
            var result = await _manager.DeleteChat(chatId, senderId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}