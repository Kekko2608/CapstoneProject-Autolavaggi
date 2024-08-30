using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CapstoneProject_Autolavaggi.Models.Auth;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Autolavaggio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Via { get; set; }
        [Required]
        public string Città { get; set; }
        [Required]
        public string CAP { get; set; }
        [Required]
        public string Telefono { get; set; }
        public string? TipoNome { get; set; }
        public Tipo? Tipo { get; set; }
        public string? Descrizione { get; set; }
        public string? Immagine { get; set; }
        [DataType(DataType.MultilineText)]
        public string? OrariDescrizione { get; set; }
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
        public List<Prenotazione>? Prenotazioni { get; set; } = new List<Prenotazione>();
        public List<Recensione>? Recensioni { get; set; } = new List<Recensione>();
        public List<Servizio>? Servizi { get; set; } = new List<Servizio>();

    }
}
