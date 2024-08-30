using CapstoneProject_Autolavaggi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_Autolavaggi.Controllers
{
    public class ServizioController : Controller
    {

        private readonly DataContext _ctx;

        public ServizioController(DataContext dataContext)
        {
            _ctx = dataContext;
        }


        public async Task<IActionResult> GetAllServizi()
        {
            var servizi = await _ctx.Servizi.ToListAsync();
            return View(servizi);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
