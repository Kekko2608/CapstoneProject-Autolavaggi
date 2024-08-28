namespace CapstoneProject_Autolavaggi.Models
{
    public class ServizioPrenotazione
    {
        public int PrenotazioneId { get; set; }
        public Prenotazione Prenotazione { get; set; }
        public int ServizioId { get; set; }
        public Servizio Servizio { get; set; }
    }
}