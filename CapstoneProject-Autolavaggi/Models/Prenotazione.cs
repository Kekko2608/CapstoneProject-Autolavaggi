using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CapstoneProject_Autolavaggi.Models.Auth;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Prenotazione
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int AutolavaggioId { get; set; }
        public Autolavaggio Autolavaggio { get; set; }
        [Required]
        public int ServizioId { get; set; }  // Associa un singolo servizio
        public Servizio Servizio { get; set; }
        public StatoPrenotazione Stato { get; set; }
        
        public enum StatoPrenotazione
        {
            InSospeso,
            Confermata,
            Completata,
            Cancellata
        }
    }
}
