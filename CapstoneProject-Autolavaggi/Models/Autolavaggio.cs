using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CapstoneProject_Autolavaggi.Models.Auth;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Autolavaggio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idd { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Via { get; set; }
        [Required]
        public string Città { get; set; }
        [Required]
        public string CAP { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        [Required]
        public string Descrizione { get; set; }
        public string Immagine { get; set; }
        [Required]
        public ICollection<Servizio> Servizi { get; set; }
        public ICollection<Prenotazione> Prenotazioni { get; set; }
        public ICollection<Recensione> Recensioni { get; set; }
        [Required]
        public ICollection<Orario> Orari { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
