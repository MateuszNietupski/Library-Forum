using Projekt.Entities.Models.DTOs.Responses;

namespace Projekt.Entities.Models
{
    public class AuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public UserInfoResponseDTO? UserInfo { get; set; }
        public List<string>? Errors { get; set; }
    }
}
