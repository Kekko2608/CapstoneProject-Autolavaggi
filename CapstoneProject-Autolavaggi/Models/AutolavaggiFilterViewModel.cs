namespace CapstoneProject_Autolavaggi.Models
{
    public class AutolavaggiFilterViewModel
    {

        public List<Tipo> Tipi { get; set; } = new List<Tipo>();
        public List<Servizio> Servizi { get; set; } = new List<Servizio>();
        public string TipoFiltro { get; set; }
        public List<int> ServiziFiltro { get; set; } = new List<int>();
        public string CittàFiltro { get; set; }
        // Risultati filtrati
        public List<Autolavaggio> Autolavaggi { get; set; } = new List<Autolavaggio>();


    }
}
