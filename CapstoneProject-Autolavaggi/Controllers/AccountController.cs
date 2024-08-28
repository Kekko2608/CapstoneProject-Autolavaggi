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
    }
}
