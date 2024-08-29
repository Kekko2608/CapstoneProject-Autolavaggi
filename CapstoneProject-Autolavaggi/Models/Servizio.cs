using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Servizio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        public int Costo { get; set; }

        [Required]
        public int Durata { get; set; }

        public int AutolavaggioId { get; set; }
        public Autolavaggio Autolavaggio { get; set; }
        public List<ServizioPrenotazione> ServizioPrenotazioni { get; set; } = new List<ServizioPrenotazione>();


    }
}
