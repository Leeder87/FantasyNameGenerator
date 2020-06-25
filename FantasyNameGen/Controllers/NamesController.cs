using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyNameGen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Controllers
{
    public class NamesController : Controller
    {
        private NamesContext db;
        private SurnamesContext dbs;
        public NamesController(NamesContext context, SurnamesContext scontext)
        {
            db = context;
            dbs = scontext;
        }
        // GET: Names
        public async Task<IActionResult> Index()
        {
            return View(await db.Names.OrderByDescending(s => s.Id).ToListAsync());
            //return RedirectToAction("GetRandom");
        }

        // GET: Names
        public async Task<IActionResult> GetRandom()
        {
            return View(await db.Names.ToListAsync());
        }

        [HttpPost]
        public IActionResult Random(char gender, char common, bool needSurname)
        {
            char all = 'у';
            List<Name> genderNames = new List<Name>();
            if (common == '3')
            {
                if (gender != 'а')
                    genderNames = db.Names.Where(p => (p.Gender == gender|| p.Gender == all)).ToList();
                else
                    genderNames = db.Names.ToList();
            }
            else
            {
                if (common == '1')
                {
                    if (gender != 'а')
                        genderNames = db.Names.Where(p => p.Common == true).Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                    else
                        genderNames = db.Names.Where(p => p.Common == true).ToList();
                }
                if (common == '2')
                {
                    List<Name> commonGenderNames = new List<Name>();
                    if (gender != 'а')
                    {
                        genderNames = db.Names.Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                        commonGenderNames = db.Names.Where(p => p.Common == true).Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                    }
                    else
                    {
                        genderNames = db.Names.ToList();
                        commonGenderNames = db.Names.Where(p => p.Common == true).ToList();
                    }
                    genderNames.AddRange(commonGenderNames);
                }
            }

            int randomNameId = CryptoRandom.Random(genderNames.Count);
            
            NameSurnameViewModel nameSurname = new NameSurnameViewModel();
            nameSurname.Name = db.Names.FirstOrDefault(p => p.Id == genderNames[randomNameId].Id);
            nameSurname.Surname = new Surname() { RomanSurname = "", CyrilSurname = "", Common = false, Id = 0 };
            if (needSurname)
            {
                List<Surname> surnamesList = new List<Surname>();
                surnamesList = dbs.Surnames.ToList();
                int randomSurnameId = CryptoRandom.Random(surnamesList.Count);
                nameSurname.Surname = dbs.Surnames.FirstOrDefault(p => p.Id == surnamesList[randomSurnameId].Id);
            }

            return PartialView("/Views/Names/_Random.cshtml", nameSurname);
            //return PartialView("/Views/Home/Contact.cshtml");
        }

        // POST: Names/ChangeCommon
        //[HttpPost]
        public void ChangeCommon(int id)
        {
            Name s = db.Names.FirstOrDefault(p => p.Id == id);
            s.Common = !s.Common;
            db.Names.Update(s);
            db.SaveChanges();
            return;
        }
        // GET: Names/Details/5
        public ActionResult Details(int id)
        {
            Name s = db.Names.FirstOrDefault(p => p.Id == id);
            return View(s);
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

        // GET: Names/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Names/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Names/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Names/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}