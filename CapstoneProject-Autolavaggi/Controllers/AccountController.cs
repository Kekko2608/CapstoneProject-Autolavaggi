using CapstoneProject_Autolavaggi.Context;
using CapstoneProject_Autolavaggi.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_Autolavaggi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;

        public AccountController(DataContext dataContext, ILogger<HomeController> logger)
        {
            _ctx = dataContext;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _ctx.User
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .SingleOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email o password non validi.");
                    return View(model);
                }

                // Crea le credenziali e il ticket di autenticazione
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role.Nome ?? "Cliente")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Mantiene il login tra le sessioni
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        public IActionResult Register()
        {
            // Supponendo che il nome del ruolo Amministratore sia "Amministratore"
            var roles = _ctx.Role
                            .Where(r => r.Nome != "Amministratore")
                            .ToList();

            ViewBag.Roles = roles;
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, int roleId)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _ctx.User.SingleOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "L'email è già in uso.");
                    return View(model);
                }

                var newUser = new User
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Password = model.Password

                };

                _ctx.User.Add(newUser);
                await _ctx.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = roleId
                };

                _ctx.UserRole.Add(userRole);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }




        [HttpGet]
        public async Task<IActionResult> Profilo()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var user = await _ctx.User
           .Include(u => u.Recensioni)
               .ThenInclude(r => r.Autolavaggio) 
           .Include(u => u.Prenotazioni)
               .ThenInclude(p => p.Autolavaggio)
                   .ThenInclude(a => a.Servizi)
           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ProfiloViewModel
            {
                Nome = user.Nome,
                Email = user.Email,
                NumeroTelefono = user.NumeroTelefono,
                Recensioni = user.Recensioni.OrderByDescending(r => r.Data).ToList(),
                Prenotazioni = user.Prenotazioni.OrderByDescending(p => p.Data).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Profilo(ProfiloViewModel model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var user = await _ctx.User.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Nome = model.Nome;
            user.Email = model.Email;
            user.NumeroTelefono = model.NumeroTelefono;

            _ctx.User.Update(user);
            await _ctx.SaveChangesAsync();

            ViewBag.Message = "Modifiche salvate con successo.";

            model.Recensioni = user.Recensioni.ToList();
            model.Prenotazioni = user.Prenotazioni.ToList();

            return View(model);
        }

    }
}