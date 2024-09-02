namespace CapstoneProject_Autolavaggi.Models
{
    public class PrenotazioniViewModel
    {
        public Autolavaggio Autolavaggio { get; set; }
        public List<Prenotazione> Prenotazioni { get; set; } = new List<Prenotazione>();
    }
}
