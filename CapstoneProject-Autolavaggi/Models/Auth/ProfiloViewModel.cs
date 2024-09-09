using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models.Auth
{
    public class ProfiloViewModel
    {
        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string NumeroTelefono { get; set; }

        public List<Recensione> Recensioni { get; set; } = new List<Recensione>();
        public List<Prenotazione> Prenotazioni { get; set; } = new List<Prenotazione>();
    }
}
