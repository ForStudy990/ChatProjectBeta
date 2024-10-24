namespace Chat.Api.Exceptions;

public class ChatNotFoundException : Exception
{
    public ChatNotFoundException() : base("Chat Not Found") {}
}