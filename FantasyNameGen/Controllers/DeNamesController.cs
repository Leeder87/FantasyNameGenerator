using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyNameGen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen.Controllers
{
    public class DeNamesController : Controller
    {
        private DeNamesContext db;
        private DeSurnamesContext dbs;
        public DeNamesController(DeNamesContext context, DeSurnamesContext scontext)
        {
            db = context;
            dbs = scontext;
        }
        // GET: Names
        public async Task<IActionResult> Index()
        {
            return View(await db.DeNames.OrderByDescending(s => s.Id).ToListAsync());
            //return RedirectToAction("GetRandom");
        }

        // GET: Names
        public async Task<IActionResult> GetRandom()
        {
            return View(await db.DeNames.ToListAsync());
        }

        [HttpPost]
        public IActionResult Random(char gender, char common, bool needSurname)
        {
            char all = 'у';
            List<DeName> genderNames = new List<DeName>();
            if (common == '3')
            {
                if (gender != 'а')
                    genderNames = db.DeNames.Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                else
                    genderNames = db.DeNames.ToList();
            }
            else
            {
                if (common == '1')
                {
                    if (gender != 'а')
                        genderNames = db.DeNames.Where(p => p.Common == true).Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                    else
                        genderNames = db.DeNames.Where(p => p.Common == true).ToList();
                }
                if (common == '2')
                {
                    List<DeName> commonGenderNames = new List<DeName>();
                    if (gender != 'а')
                    {
                        genderNames = db.DeNames.Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                        commonGenderNames = db.DeNames.Where(p => p.Common == true).Where(p => (p.Gender == gender || p.Gender == all)).ToList();
                    }
                    else
                    {
                        genderNames = db.DeNames.ToList();
                        commonGenderNames = db.DeNames.Where(p => p.Common == true).ToList();
                    }
                    genderNames.AddRange(commonGenderNames);
                }
            }

            int randomNameId = CryptoRandom.Random(genderNames.Count);

            DeNameDeSurnameViewModel nameSurname = new DeNameDeSurnameViewModel();
            nameSurname.DeName = db.DeNames.FirstOrDefault(p => p.Id == genderNames[randomNameId].Id);
            nameSurname.DeSurname = new DeSurname() { RomanSurname = "", CyrilSurname = "", Common = false, Id = 0 };
            if (needSurname)
            {
                List<DeSurname> surnamesList = new List<DeSurname>();
                surnamesList = dbs.DeSurnames.ToList();
                int randomSurnameId = CryptoRandom.Random(surnamesList.Count);
                nameSurname.DeSurname = dbs.DeSurnames.FirstOrDefault(p => p.Id == surnamesList[randomSurnameId].Id);
            }

            return PartialView("/Views/DeNames/_Random.cshtml", nameSurname);
            //return PartialView("/Views/Home/Contact.cshtml");
        }

        // POST: Names/ChangeCommon
        //[HttpPost]
        public void ChangeCommon(int id)
        {
            DeName s = db.DeNames.FirstOrDefault(p => p.Id == id);
            s.Common = !s.Common;
            db.DeNames.Update(s);
            db.SaveChanges();
            return;
        }
        // GET: Names/Details/5
        public ActionResult Details(int id)
        {
            DeName s = db.DeNames.FirstOrDefault(p => p.Id == id);
            return View(s);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DeName name)
        {
            DeName nameTrimmed = name;
            nameTrimmed.CyrilName = nameTrimmed.CyrilName.Trim();
            nameTrimmed.RomanName = nameTrimmed.RomanName.Trim();
            if (!string.IsNullOrWhiteSpace(nameTrimmed.Variants))
            {
                if (nameTrimmed.Variants.Contains("Источник: http://kurufin.ru"))
                    nameTrimmed.Variants = nameTrimmed.Variants.Remove(nameTrimmed.Variants.IndexOf("Источник: http://kurufin.ru"));
                while (nameTrimmed.Variants.Contains("Pronunciation by"))
                {
                    string toDelete = nameTrimmed.Variants.Substring(nameTrimmed.Variants.IndexOf("Pronunciation by"));
                    int countToDelete = toDelete.IndexOf(',');
                    nameTrimmed.Variants = nameTrimmed.Variants.Remove(nameTrimmed.Variants.IndexOf("Pronunciation by"), countToDelete);
                }
                while (nameTrimmed.Variants.Contains("произнёс пользователь"))
                {
                    string toDelete = nameTrimmed.Variants.Substring(nameTrimmed.Variants.IndexOf("произнёс пользователь"));
                    int countToDelete = toDelete.IndexOf(") ,") + 2;
                    nameTrimmed.Variants = nameTrimmed.Variants.Remove(nameTrimmed.Variants.IndexOf("произнёс пользователь"), countToDelete);
                }
            }
            if (!string.IsNullOrWhiteSpace(nameTrimmed.Description) && nameTrimmed.Description.Contains("Источник: http://kurufin.ru"))
                nameTrimmed.Description = nameTrimmed.Description.Remove(nameTrimmed.Description.IndexOf("Источник: http://kurufin.ru"));
            nameTrimmed.Variants = nameTrimmed.Variants?.Trim();
            nameTrimmed.Description = nameTrimmed.Description?.Trim();
            db.DeNames.Add(nameTrimmed);
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