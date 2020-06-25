using System.Linq;
using System.Threading.Tasks;
using FantasyNameGen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Controllers
{
    public class DeSurnamesController : Controller
    {
        private DeSurnamesContext dbs;
        public DeSurnamesController(DeSurnamesContext context)
        {
            dbs = context;
        }

        // GET: Surnames
        public async Task<IActionResult> Index()
        {
            return View(await dbs.DeSurnames.OrderByDescending(s => s.Id).ToListAsync());
            //return RedirectToAction("GetRandom");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DeSurname surname)
        {
            DeSurname surnameTrimmed = surname;
            if (surnameTrimmed.CyrilSurname.Contains("Источник: "))
                surnameTrimmed.CyrilSurname = surnameTrimmed.CyrilSurname.Remove(surnameTrimmed.CyrilSurname.IndexOf("Источник: "));
            if (surnameTrimmed.RomanSurname.Contains("Источник: "))
                surnameTrimmed.RomanSurname = surnameTrimmed.RomanSurname.Remove(surnameTrimmed.RomanSurname.IndexOf("Источник: "));
            surnameTrimmed.CyrilSurname = surnameTrimmed.CyrilSurname.Trim();
            surnameTrimmed.RomanSurname = surnameTrimmed.RomanSurname.Trim();

            dbs.DeSurnames.Add(surnameTrimmed);
            //dbs.DeSurnames.Add(surname);
            await dbs.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // POST: Surnames/ChangeCommon
        //[HttpPost]
        public void ChangeCommon(int id)
        {
            DeSurname s = dbs.DeSurnames.FirstOrDefault(p => p.Id == id);
            s.Common = !s.Common;
            dbs.DeSurnames.Update(s);
            dbs.SaveChanges();
            return;
        }


    }
}