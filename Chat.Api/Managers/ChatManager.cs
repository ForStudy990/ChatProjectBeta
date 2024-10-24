using Chat.Api.DTOs;
using Chat.Api.Entities;
using Chat.Api.Repositories.Abstract;
using Chat.Api.Extensions;

namespace Chat.Api.Managers;

public class ChatManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ChatManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ChatDto>> GetAllChats()
    {
        var chats = await _unitOfWork.ChatRepository.GetAllChats();
        return chats.ParseToDto();
    }

    public async Task<List<ChatDto>> GetAllUserChats(Guid userId)
    {
        var chats = await _unitOfWork.ChatRepository.GetAllUserChats(userId);
        return chats.ParseToDto();
    }

    public async Task<ChatDto> GetUserChatById(Guid chatId, Guid userId)
    {
        var chat = await _unitOfWork.ChatRepository.GetUserChatById(chatId, userId);
        return chat.ParseToDto();
    }

    public async Task<ChatDto> AddOrEnterChat(Guid receiverId, Guid senderId)
    {
        var (check, chat) = await _unitOfWork.ChatRepository.CheckCheckExist(receiverId, senderId);
        if(check)
            return chat!.ParseToDto();
        var receiver = await _unitOfWork.UserRepository.GetUserById(receiverId);
        var sender = await _unitOfWork.UserRepository.GetUserById(senderId);
        var receiversFullName = receiver.FirstName + " " + receiver.LastName;
        var senderFullName = sender.FirstName + " " + sender.LastName;
        List<string> names = new List<string>();
        names.Add(receiver.FirstName);
        names.Add(sender.FirstName);
        chat = new Entities.Chat()
        {
            ChatNames = names
        };
        await _unitOfWork.ChatRepository.AddChat(chat);
        var receiversUserChat = new UserChat()
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            ChatId = chat.Id
        };
        await _unitOfWork.UserChatRepository.AddUserChat(receiversUserChat);
        var sendersUserChat = new UserChat()
        {
            SenderId = receiverId,
            ReceiverId = senderId,
            ChatId = chat.Id
        };
        await _unitOfWork.UserChatRepository.AddUserChat(sendersUserChat);
        return chat.ParseToDto();
    }

    public async Task<string> DeleteChat(Guid chatId, Guid userId)
    {
        var user = await _unitOfWork.UserRepository.GetUserById(userId);
        var chat = await _unitOfWork.ChatRepository.GetUserChatById(userId, chatId);
        await _unitOfWork.ChatRepository.DeleteChat(chat);
        return "Successfully deleted chat";
    }
}