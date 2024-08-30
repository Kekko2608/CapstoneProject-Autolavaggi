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
            var viewmodel = new AutolavaggioViewModel
            {
                Tipi = await _ctx.Tipi.ToListAsync(),
                Servizi = await _ctx.Servizi.ToListAsync(),
                
            };

            return View(viewmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreaAutolavaggio(AutolavaggioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var autolavaggio = new Autolavaggio
                {
                    Nome = model.Autolavaggio.Nome,
                    Via = model.Autolavaggio.Via,
                    Città = model.Autolavaggio.Città,
                    CAP = model.Autolavaggio.CAP,
                    Telefono = model.Autolavaggio.Telefono,
                    Descrizione = model.Autolavaggio.Descrizione,
                    Immagine = model.Autolavaggio.Immagine,
                    OrariDescrizione = model.Autolavaggio.OrariDescrizione,
                    TipoNome = model.Autolavaggio.TipoNome,
                    Servizi= await _ctx.Servizi
                    .Where(s => model.SelectedServizi.Contains(s.Id))
                    .ToListAsync()
                    
                };
            
                _ctx.Autolavaggi.Add(autolavaggio);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            model.Servizi = await _ctx.Servizi.ToListAsync();
            model.Tipi = await _ctx.Tipi.ToListAsync();
            return View(model);
        }



        public async Task<IActionResult> GetAllAutolavaggi()
        {
            var autolavaggi = await _ctx.Autolavaggi
                .Select(a => new
                {
                    a.Id,
                    a.Nome,
                    a.Immagine
                })
                .ToListAsync();

            return View(autolavaggi);
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



        public IActionResult Index()
        {
            return View();
        }
    }
}
