using CapstoneProject_Autolavaggi.Context;
using CapstoneProject_Autolavaggi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_Autolavaggi.Controllers
{
    public class AutolavaggioController : Controller
    {

        private readonly DataContext _ctx;

        public AutolavaggioController(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> CreaAutolavaggio()
        {
            var tipi = await _ctx.Tipi.ToListAsync();
            var servizi = await _ctx.Servizi.ToListAsync(); 
            ViewBag.Tipi = tipi;
            ViewBag.Servizi = servizi;

            var model = new Autolavaggio
            {
                ServiziSelezionati = new List<int>()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreaAutolavaggio(Autolavaggio autolavaggio, List<int> ServiziSelezionati)
        {
            if (ModelState.IsValid)
            {
                // Aggiungi l'autolavaggio al database
                _ctx.Autolavaggi.Add(autolavaggio);
                await _ctx.SaveChangesAsync();

                // Associa i servizi selezionati all'autolavaggio
                if (ServiziSelezionati != null && ServiziSelezionati.Any())
                {
                    var servizi = await _ctx.Servizi
                        .Where(s => ServiziSelezionati.Contains(s.Id))
                        .ToListAsync();

                    foreach (var servizio in servizi)
                    {
                        autolavaggio.Servizi.Add(servizio);
                    }

                    await _ctx.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }

            // Ricarica i tipi e i servizi per la vista in caso di errore
            var tipi = await _ctx.Tipi.ToListAsync();
            ViewBag.Tipi = tipi;

            var serviziList = await _ctx.Servizi.ToListAsync();
            ViewBag.Servizi = serviziList;

            return View(autolavaggio);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
