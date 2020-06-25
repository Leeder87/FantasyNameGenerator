using System.Linq;
using System.Threading.Tasks;
using FantasyNameGen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Controllers
{
    public class SurnamesController : Controller
    {
        private SurnamesContext dbs;
        public SurnamesController(SurnamesContext context)
        {
            dbs = context;
        }

        // GET: Surnames
        public async Task<IActionResult> Index()
        {
            return View(await dbs.Surnames.OrderByDescending(s => s.Id).ToListAsync());
            //return RedirectToAction("GetRandom");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Surname surname)
        {
            dbs.Surnames.Add(surname);
            await dbs.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // POST: Surnames/ChangeCommon
        //[HttpPost]
        public void ChangeCommon(int id)
        {
            Surname s = dbs.Surnames.FirstOrDefault(p => p.Id == id);
            s.Common = !s.Common;
            dbs.Surnames.Update(s);
            dbs.SaveChanges();
            return;
        }


    }
}