using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models.Auth
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public string NumeroTelefono { get; set; }
        public ICollection<Prenotazione> Prenotazioni { get; set; }
        public ICollection<Recensione> Recensioni { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
