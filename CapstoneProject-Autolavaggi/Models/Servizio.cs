using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Servizio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        [Required]
        public int Costo { get; set; }
        [Required]
        public int Durata { get; set; }

    }
}
