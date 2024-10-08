﻿using CapstoneProject_Autolavaggi.Context;
using CapstoneProject_Autolavaggi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapstoneProject_Autolavaggi.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly DataContext _ctx;

        public PrenotazioneController(DataContext context)
        {
            _ctx = context;
        }
    
        public async Task<IActionResult> Prenotazione(int servizioId, int autolavaggioId)
        {
            // Recupera il servizio e l'autolavaggio dal database
            var servizio = await _ctx.Servizi.FirstOrDefaultAsync(s => s.Id == servizioId);
            var autolavaggio = await _ctx.Autolavaggi.FirstOrDefaultAsync(a => a.Id == autolavaggioId);

            // Verifica che il servizio e l'autolavaggio esistano
            if (servizio == null || autolavaggio == null)
            {          
                return NotFound("Servizio o Autolavaggio non trovato");
            }

            // Crea un ViewModel per passare i dati alla vista
            var model = new AutolavaggioViewModel
            {
                Autolavaggio = autolavaggio,
                NuovaPrenotazione = new Prenotazione
                {
                    ServizioId = servizioId,
                    AutolavaggioId = autolavaggioId
                },
                Servizi = new List<Servizio> { servizio }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfermaPrenotazione(AutolavaggioViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Verifica se il servizio e l'autolavaggio esistono
            var servizio = await _ctx.Servizi.FirstOrDefaultAsync(s => s.Id == model.NuovaPrenotazione.ServizioId);
            var autolavaggio = await _ctx.Autolavaggi.FirstOrDefaultAsync(a => a.Id == model.NuovaPrenotazione.AutolavaggioId);

            if (servizio == null || autolavaggio == null)
            {
                return NotFound("Servizio o Autolavaggio non trovato");
            }

            var prenotazione = new Prenotazione
            {
                UserId = int.Parse(userId),  
                ServizioId = model.NuovaPrenotazione.ServizioId,
                AutolavaggioId = model.NuovaPrenotazione.AutolavaggioId,
                Data = model.NuovaPrenotazione.Data
            };

            _ctx.Prenotazioni.Add(prenotazione);
            await _ctx.SaveChangesAsync(); 

            TempData["SuccessMessage"] = "Prenotazione Confermata";

            return RedirectToAction("GetAllAutolavaggi", "Autolavaggio");

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
