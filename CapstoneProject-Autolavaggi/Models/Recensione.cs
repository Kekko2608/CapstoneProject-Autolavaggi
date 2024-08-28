using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CapstoneProject_Autolavaggi.Models.Auth;

namespace CapstoneProject_Autolavaggi.Models
{
    public class Recensione
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Titolo { get; set; }
        [Required]
        public int Voto { get; set; }
        [Required]
        public string Commento { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int AutolavaggioId { get; set; }
        public Autolavaggio Autolavaggio { get; set; }
    }
}