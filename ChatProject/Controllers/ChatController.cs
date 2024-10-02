using ChatProject.Entities;
using ChatProject.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChatController : Controller
{
    private ChatManager _manager;

    public ChatController(ChatManager manager)
    {
        _manager = manager;
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
    public async Task<IActionResult> AddOrEnterChat([FromBody] Guid reciverId ,Guid senderId)
    {
        var chat = await _manager.AddOrEnterChat(reciverId, senderId);
        return Ok(chat);
    }

    [HttpDelete("{chatId:Guid}")]
    public async Task<IActionResult> DeleteChat([FromRoute] Guid chatId, Guid senderId)
    {
        try
        {
            var result = await _manager.DeleteChat(chatId, senderId);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}