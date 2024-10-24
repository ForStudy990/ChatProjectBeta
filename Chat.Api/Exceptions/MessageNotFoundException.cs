namespace Chat.Api.Exceptions;

public class MessageNotFoundException : Exception
{
    public MessageNotFoundException():base("Message not Found"){}
}