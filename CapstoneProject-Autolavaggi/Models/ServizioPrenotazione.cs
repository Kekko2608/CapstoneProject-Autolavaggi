using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models
{
    public class ServizioPrenotazione
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PrenotazioneId { get; set; }
        public Prenotazione Prenotazione { get; set; }
        public int ServizioId { get; set; }
        public Servizio Servizio { get; set; }
    }
}