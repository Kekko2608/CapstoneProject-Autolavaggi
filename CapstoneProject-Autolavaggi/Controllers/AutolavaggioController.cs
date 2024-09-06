using CapstoneProject_Autolavaggi.Context;
using CapstoneProject_Autolavaggi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CapstoneProject_Autolavaggi.Controllers
{
    [Authorize]
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
            var viewmodel = new AutolavaggioViewModel
            {
                Autolavaggio = new Autolavaggio(),
                Tipi = await _ctx.Tipi.ToListAsync(),
                Servizi = await _ctx.Servizi.ToListAsync(),

            };

            return View(viewmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreaAutolavaggio(AutolavaggioViewModel model)
        {
            
                // Recupera l'email dell'utente autenticato
                var userEmail = User.Identity.Name;
                // Trova l'utente nel database usando l'email
                var user = await _ctx.User.SingleOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return Unauthorized(); 
                }

                var autolavaggio = new Autolavaggio
                {
                    Nome = model.Autolavaggio.Nome,
                    Via = model.Autolavaggio.Via,
                    Città = model.Autolavaggio.Città,
                    CAP = model.Autolavaggio.CAP,
                    GoogleMapsUrl = model.Autolavaggio.GoogleMapsUrl,
                    Telefono = model.Autolavaggio.Telefono,
                    Descrizione = model.Autolavaggio.Descrizione,
                    Immagine = model.Autolavaggio.Immagine,
                    OrariDescrizione = model.Autolavaggio.OrariDescrizione,
                    TipoNome = model.Autolavaggio.TipoNome,
                    Servizi = await _ctx.Servizi
                        .Where(s => model.SelectedServizi.Contains(s.Id))
                        .ToListAsync(),
                    OwnerId = user.Id 
                };

                _ctx.Autolavaggi.Add(autolavaggio);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
          
        }




        public async Task<IActionResult> GetAllAutolavaggi(string tipoFiltro, List<int> serviziFiltro, string cittàFiltro)
        {
            // Recupera tutti i tipi e servizi per il filtro
            var tipi = await _ctx.Tipi.ToListAsync();
            var servizi = await _ctx.Servizi.ToListAsync();

            // Crea la query per filtrare gli autolavaggi
            var query = _ctx.Autolavaggi.AsQueryable();

            if (!string.IsNullOrEmpty(tipoFiltro))
            {
                query = query.Where(a => a.TipoNome == tipoFiltro);
            }

            if (serviziFiltro != null && serviziFiltro.Any())
            {
                query = query.Where(a => a.Servizi.Any(s => serviziFiltro.Contains(s.Id)));
            }

            if (!string.IsNullOrEmpty(cittàFiltro))
            {
                query = query.Where(a => a.Città == cittàFiltro);
            }

            // Recupera gli autolavaggi filtrati
            var autolavaggi = await query.ToListAsync();

            // Crea il ViewModel
            var viewModel = new AutolavaggiFilterViewModel
            {
                Autolavaggi = autolavaggi,
                Tipi = tipi,
                Servizi = servizi,
                TipoFiltro = tipoFiltro,
                ServiziFiltro = serviziFiltro,
                CittàFiltro = cittàFiltro
            };

            return View(viewModel);
        }






        public async Task<IActionResult> DettagliAutolavaggio(int id)
        {
            var autolavaggio = await _ctx.Autolavaggi
                .Include(a => a.Servizi)
                .Include(a => a.Tipo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autolavaggio == null)
            {
                return NotFound();
            }

            var viewModel = new AutolavaggioViewModel
            {
                Autolavaggio = autolavaggio,
                Servizi = autolavaggio.Servizi,
                Tipi = new List<Tipo> { autolavaggio.Tipo }
            };

            return View(viewModel);
        }



        public async Task<IActionResult> DettagliAutolavaggioGestore()
        {
            // Recupera l'email dell'utente loggato
            var userEmail = User.Identity.Name;

            if (userEmail == null)
            {
                return Unauthorized();
            }

            // Trova l'utente corrispondente all'email
            var user = await _ctx.User.SingleOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound("Utente non trovato.");
            }

            // Trova l'autolavaggio associato all'utente loggato
            var autolavaggio = await _ctx.Autolavaggi
                .Include(a => a.Servizi)
                .Include(a => a.Tipo)
                .FirstOrDefaultAsync(a => a.OwnerId == user.Id);

            if (autolavaggio == null)
            {
                return NotFound("Autolavaggio non trovato.");
            }

            var viewModel = new AutolavaggioViewModel
            {
                Autolavaggio = autolavaggio,
                Servizi = autolavaggio.Servizi,
                Tipi = new List<Tipo> { autolavaggio.Tipo },
                Recensioni = await _ctx.Recensioni.Where(r => r.AutolavaggioId == autolavaggio.Id).ToListAsync(),
                Prenotazioni = await _ctx.Prenotazioni.Where(p => p.AutolavaggioId == autolavaggio.Id).ToListAsync()
            };

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> ModificaAutolavaggio(int id)
        {
            var autolavaggio = await _ctx.Autolavaggi
                .Include(a => a.Servizi)
                .Include(a => a.Tipo)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autolavaggio == null)
            {
                return NotFound();
            }

            var viewModel = new AutolavaggioViewModel
            {
                Autolavaggio = autolavaggio,
                Servizi = await _ctx.Servizi.ToListAsync(),
                Tipi = await _ctx.Tipi.ToListAsync(),
                SelectedServizi = autolavaggio.Servizi.Select(s => s.Id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificaAutolavaggio(AutolavaggioViewModel model)
        {
           
                var autolavaggio = await _ctx.Autolavaggi
                    .Include(a => a.Servizi) // Carica anche i servizi per la rimozione e aggiunta
                    .FirstOrDefaultAsync(a => a.Id == model.Autolavaggio.Id);

                if (autolavaggio == null)
                {
                    return NotFound();
                }

                // Aggiorna i dati dell'autolavaggio
                autolavaggio.Nome = model.Autolavaggio.Nome;
                autolavaggio.Via = model.Autolavaggio.Via;
                autolavaggio.Città = model.Autolavaggio.Città;
                autolavaggio.CAP = model.Autolavaggio.CAP;
                autolavaggio.GoogleMapsUrl = model.Autolavaggio.GoogleMapsUrl;
                autolavaggio.Telefono = model.Autolavaggio.Telefono;
                autolavaggio.Descrizione = model.Autolavaggio.Descrizione;
                autolavaggio.Immagine = model.Autolavaggio.Immagine;
                autolavaggio.OrariDescrizione = model.Autolavaggio.OrariDescrizione;
                autolavaggio.TipoNome = model.Autolavaggio.TipoNome;

                // Aggiorna i servizi
                var serviziToRemove = autolavaggio.Servizi.Where(s => !model.SelectedServizi.Contains(s.Id)).ToList();
                var serviziToAdd = await _ctx.Servizi.Where(s => model.SelectedServizi.Contains(s.Id)).ToListAsync();

                // Rimuove i servizi non selezionati
                foreach (var servizio in serviziToRemove)
                {
                    autolavaggio.Servizi.Remove(servizio);
                }

                // Aggiunge i nuovi servizi
                foreach (var servizio in serviziToAdd)
                {
                    if (!autolavaggio.Servizi.Contains(servizio))
                    {
                        autolavaggio.Servizi.Add(servizio);
                    }
                }

                // Salva le modifiche nel database
                await _ctx.SaveChangesAsync();

                return RedirectToAction("DettagliAutolavaggio", new { id = autolavaggio.Id });
            

           
        }


    }
}
