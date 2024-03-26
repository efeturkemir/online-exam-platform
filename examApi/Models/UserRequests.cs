using System.ComponentModel.DataAnnotations;

namespace examApi.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression("^(admin|user)$", ErrorMessage = "Role must be 'admin' or 'user'.")] 
        public string Role { get; set; }
    }
}
