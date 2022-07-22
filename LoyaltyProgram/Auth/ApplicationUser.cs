using Microsoft.AspNetCore.Identity;

namespace LoyaltyProgram.Auth
{
    public class ApplicationUser
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public Role Role { get; set; }
    }
}
