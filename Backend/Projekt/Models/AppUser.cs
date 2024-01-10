using Microsoft.AspNetCore.Identity;

namespace Projekt.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<ForumPost>? Posts { get; set; } = new List<ForumPost>();
        public ICollection<ForumComment>? Comments { get; set; } = new List<ForumComment>();
    }
}
