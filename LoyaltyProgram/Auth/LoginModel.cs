using System.ComponentModel.DataAnnotations;

namespace LoyaltyProgram.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
    }
}
