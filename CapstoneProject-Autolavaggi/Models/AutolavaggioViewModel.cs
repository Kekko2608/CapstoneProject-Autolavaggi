namespace CapstoneProject_Autolavaggi.Models
{
    public class AutolavaggioViewModel
    {
        public Autolavaggio Autolavaggio { get; set; }
        public List<Servizio> Servizi { get; set; } = new List<Servizio>();
        public List<Tipo> Tipi { get; set; } = new List<Tipo>();
        public List<int> SelectedServizi { get; set; } = new List<int>();
    }
}
