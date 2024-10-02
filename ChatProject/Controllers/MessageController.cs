using ChatProject.Entities;
using ChatProject.Exceptions;
using ChatProject.Managers;
using ChatProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers;
[Route("api/[controller]")]
[ApiController]

public class MessageController : Controller
{
    private MessageManager _messageManager;

    public MessageController(MessageManager messageManager)
    {
        _messageManager = messageManager;
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

    [HttpGet]
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
    public async Task<IActionResult> SendTextMessage(Guid userId, Guid chatId,[FromBody] TextModel model)
    {
        try
        {
           var message = await _messageManager.SendTextMessage(userId, chatId, model);
           return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("send-file-message")]
    public async Task<IActionResult> SendFileMessage(Guid userId, Guid chatId, [FromBody] FileModel model)
    {
        try
        {
            var result = await _messageManager.SendFileMessage(userId, chatId, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}