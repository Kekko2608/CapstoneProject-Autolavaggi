﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneProject_Autolavaggi.Context;
using CapstoneProject_Autolavaggi.Models;
using System.Security.Claims;

public class RecensioneController : Controller
{
    private readonly DataContext _ctx;

    public RecensioneController(DataContext context)
    {
        _ctx = context;
    }

    
    public async Task<IActionResult> Recensioni(int id)
    {
        var autolavaggio = await _ctx.Autolavaggi
            .Include(a => a.Recensioni)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (autolavaggio == null)
        {
            return NotFound();
        }

        var viewModel = new AutolavaggioViewModel
        {
            Autolavaggio = autolavaggio,
            Recensioni = autolavaggio.Recensioni
            .OrderByDescending(r => r.Data) // Ordinare per data, più recente prima
            .ToList(),
            NuovaRecensione = new Recensione { AutolavaggioId = id }
        };

        return View(viewModel);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AggiungiRecensione(AutolavaggioViewModel model)
    {
       
        try
        {
            // Recupera l'ID dell'utente corrente dai claim
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Utente non autenticato." });
            }

            // Recupera l'autolavaggio
            var autolavaggio = await _ctx.Autolavaggi.FindAsync(model.NuovaRecensione.AutolavaggioId);
            if (autolavaggio == null)
            {
                return NotFound(new { message = "Autolavaggio non trovato." });
            }

            var recensione = new Recensione
            {
                Titolo = model.NuovaRecensione.Titolo,
                Voto = model.NuovaRecensione.Voto,
                Commento = model.NuovaRecensione.Commento,
                Data = DateTime.Now,
                UserId = int.Parse(userId),  
                AutolavaggioId = model.NuovaRecensione.AutolavaggioId
            };

            _ctx.Recensioni.Add(recensione);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Recensioni", new { id = model.NuovaRecensione.AutolavaggioId });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.ToString());
            return StatusCode(500, new { message = "Si è verificato un problema durante l'aggiunta della recensione." });
        }
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminaRecensione(int recensioneId)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized();
        }

        var recensione = await _ctx.Recensioni
            .FirstOrDefaultAsync(r => r.Id == recensioneId && r.UserId == userId);

        if (recensione == null)
        {
            return NotFound(); 
        }

        _ctx.Recensioni.Remove(recensione);
        await _ctx.SaveChangesAsync();

        return RedirectToAction("Profilo", "Account"); 
    }
}
