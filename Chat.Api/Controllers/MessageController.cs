using Chat.Api.Exceptions;
using Chat.Api.Managers;
using Chat.Api.Models;
using Chat.Api.Entities;
using Chat.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : Controller
{
    private readonly MessageManager _messageManager;
    private readonly UserHelper _userHelper;

    public MessageController(MessageManager messageManager, UserHelper userHelper)
    {
        _messageManager = messageManager;
        _userHelper = userHelper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessages(Guid chatId)
    {
        var messages = await _messageManager.GetAllMessages();
        return Ok(messages);
    }

    [HttpGet("api/messages/{id:int}")]
    public async Task<IActionResult> GetMessagesById(Guid chatid,int messageId)
    {
        try
        {
            var messages = await _messageManager.GetChatMessageById(chatid, messageId);
            return Ok(messages);
        }
        catch (MessageNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet("get-chat-messages")]
    public async Task<IActionResult> GetChatMessages(Guid chatId)
    {
        try
        {
            var messages = await _messageManager.GetChatMessages(chatId);
            return Ok(messages);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("send-text-message")]
    public async Task<IActionResult> SendTextMessage(Guid chatId,[FromBody] TextModel model)
    {
        try
        { 
            var userId = _userHelper.GetUserId();
           var message = await _messageManager.SendTextMessage(chatId, userId, model);
           return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("send-file-message")]
    public async Task<IActionResult> SendFileMessage(Guid chatId, [FromBody] FileModel model)
    {
        try
        {
            var userId = _userHelper.GetUserId();
            var result = await _messageManager.SendFileMessage(userId, chatId, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}