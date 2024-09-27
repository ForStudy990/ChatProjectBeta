namespace ChatProject.Exceptions;

public class UserChatNotFoundException : Exception
{
    public UserChatNotFoundException():base("UserChat Not Found"){}
}