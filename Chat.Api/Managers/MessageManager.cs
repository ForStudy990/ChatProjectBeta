using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Models;
using Chat.Api.Repositories.Abstract;
using Chat.Api.Extensions;
using Chat.Api.Repositories;

namespace Chat.Api.Managers;

public class MessageManager
{
    private readonly IUnitOfWork _userRepository;
    private readonly IHostEnvironment _hostingEnvironment;

    public MessageManager(IUnitOfWork userRepository, IHostEnvironment hostingEnvironment)
    {
        _userRepository = userRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<List<MessageDto>> GetAllMessages()
    {
        var messages = await _userRepository.MessageRepository.GetAllMessages();
        return messages.ParseToDto();
    }

    public async Task<List<MessageDto>> GetChatMessages(Guid chatId)
    {
        var messages = await _userRepository.MessageRepository.GetChatMessages(chatId);
        return messages.ParseToDto();
    }

    public async Task<MessageDto> GetChatMessageById(Guid chatId, int messageId)
    {
        var message = await _userRepository.MessageRepository.GetChatMessageById(chatId, messageId);
        return message.ParseToDto();
    }

    public async Task<MessageDto> SendTextMessage(Guid chatId, Guid userId, TextModel model)
    {
        await IsUserInChat(userId, chatId);
        var user = await _userRepository.UserRepository.GetUserById(userId);
        var message = new Message()
        {
            Text = model.Text,
            FromUserId = userId,
            FromUserName = user.UserName,
            ChatId = chatId
        };
        await _userRepository.MessageRepository.AddMessage(message);
        return message.ParseToDto();
    }

    public async Task<MessageDto> SendFileMessage(Guid chatId, Guid userId, FileModel model)
    {
        var user = await _userRepository.UserRepository.GetUserById(userId);
        await IsUserInChat(userId, chatId);
        var ms = new MemoryStream();
        await model.File.CopyToAsync(ms);
        var data = ms.ToArray();
        var fileUrl = GetFilePath();
        await File.WriteAllBytesAsync(fileUrl, data);

        var content = new Content()
        {
            Caption = model.Caption,
            FileUrl = fileUrl,
            Type = model.File.ContentType
            
        };
        var message = new Message()
        {
            FromUserId = userId,
            FromUserName = user.UserName,
            ChatId = chatId,
            Content = content,
            ContentId = content.Id
        };
        await _userRepository.MessageRepository.AddMessage(message);
        return message.ParseToDto();
    }
    

    private async Task IsUserInChat(Guid userId, Guid chatId)
    {
        await _userRepository.UserChatRepository.GetUserChat(userId, chatId);
    }

    private string GetFilePath()
    {
        var generalPath = _hostingEnvironment.ContentRootPath;
        var name = Guid.NewGuid();
        var fileName = generalPath + "\\wwwroot\\MessageFiles\\" + name;
        return fileName;
    }
}