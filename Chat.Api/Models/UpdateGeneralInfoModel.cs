namespace Chat.Api.Models;

public class UpdateGeneralInfoModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public byte? Age { get; set; }
    
    public string? Gender { get; set; }
}