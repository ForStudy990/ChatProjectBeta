using Chat.Api.DTOs;
using Chat.Api.Entities;
using Mapster;

namespace Chat.Api.Extensions;

public static class ParseToDtoExtension
{
    public static UserDto ParseToDto(this User user)
    {
        UserDto userDto = user.Adapt<UserDto>();
        return userDto;
    }

    public static List<UserDto> ParseToDto(this List<User>? users)
    {
        var dtos = new List<UserDto>();
        if (users == null || users.Count == 0)
            return dtos;
        dtos.AddRange(users.Select(user => user.ParseToDto()));
        return dtos;
    }

    public static ChatDto ParseToDto(this Entities.Chat chat)
    {
        var chatdto = chat.Adapt<ChatDto>();
        return chatdto;
    }

    public static List<ChatDto> ParseToDto(this List<Entities.Chat>? chats)
    {
        var dtos = new List<ChatDto>();
        if (chats == null || chats.Count == 0)
            return dtos;
        dtos.AddRange(chats.Select(chat => chat.ParseToDto()));
        return dtos;
    }
    public static MessageDto ParseToDto(this Message message)
    {
        var dto = message.Adapt<MessageDto>();
        return dto;
    }

    public static List<MessageDto> ParseToDto(this List<Message>? messages)
    {
        var dtos = new List<MessageDto>();
        if (messages == null || messages.Count == 0)
            return dtos;
        dtos.AddRange(messages.Select(message => message.ParseToDto()));
        return dtos;
    }
    
    
}