using Microsoft.AspNetCore.Identity;

namespace Projekt.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Post>? Posts { get; set; } = new List<Post>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
