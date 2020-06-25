using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FantasyNameGen.Models; // пространство имен моделей и контекста данных
 
namespace FantasyNameGen.Controllers
{
    public class HomeController : Controller
    {
        private NamesContext db;
        public HomeController(NamesContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Names.OrderByDescending(s => s.Id).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Name name)
        {
            db.Names.Add(name);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}