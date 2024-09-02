﻿using CapstoneProject_Autolavaggi.Context;
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
                    Servizi = await _ctx.Servizi
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



        public async Task<IActionResult> DettagliAutolavaggioGestore(int id)
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
                Tipi = new List<Tipo> { autolavaggio.Tipo },
                Recensioni = await _ctx.Recensioni.Where(r => r.AutolavaggioId == id).ToListAsync(),
                Prenotazioni = await _ctx.Prenotazioni.Where(p => p.AutolavaggioId == id).ToListAsync()
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
            

            // Se il modello non è valido, ricarica le liste dei servizi e dei tipi
            model.Servizi = await _ctx.Servizi.ToListAsync();
            model.Tipi = await _ctx.Tipi.ToListAsync();

            return View(model);
        }


    }
}
