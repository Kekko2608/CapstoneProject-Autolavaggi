using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
