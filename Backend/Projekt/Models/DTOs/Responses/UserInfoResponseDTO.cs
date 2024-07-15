namespace Projekt.Models.DTOs.Responses;

public class UserInfoResponseDTO
{
    public string UserName { get; set; }
    public string Id { get; set; }
    public IList<string> Roles { get; set; }
}