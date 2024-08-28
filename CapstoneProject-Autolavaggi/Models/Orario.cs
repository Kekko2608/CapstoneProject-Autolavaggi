using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Orario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public GiorniSettimana GiornoSettimana { get; set; }
        public TimeSpan Dalle { get; set; }
        public TimeSpan Alle { get; set; }
        public int AutolavaggioId { get; set; }
        public Autolavaggio Autolavaggio { get; set; }

        public enum GiorniSettimana
        {
            Lunedì,
            Martedì,
            Mercoledì,
            Giovedì,
            Venerdì,
            Sabato,
            Domenica
        }
    }
}