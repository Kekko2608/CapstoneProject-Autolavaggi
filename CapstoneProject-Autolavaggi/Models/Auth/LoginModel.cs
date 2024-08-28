using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models.Auth
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        [MaxLength(80)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
