using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab9_10.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab9_10.Controllers
{
    public class HomeController : Controller
    {
        private HumanContext db;
        public HomeController(HumanContext context)
        {
            db = context;
        }

  
        public async Task<IActionResult> Index()
        {
            return View(await db.Humans.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Human human)
        {

            if (string.IsNullOrEmpty(human.Name))
            {
                ModelState.AddModelError("Name", "Некорректное имя");
            }
            else if (human.Name.Length > 10)
            {
                ModelState.AddModelError("Name", "Недопустимая длина строки");
            }


                if (ModelState.IsValid)
            {
                db.Humans.Add(human);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Запрос не прошел валидацию";
                return View(human);
            }

        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Human human = await db.Humans.FirstOrDefaultAsync(p => p.Id == id);
                if (human != null)
                    return View(human);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Human human)
        {
            db.Humans.Update(human);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");//
        }



        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Human human = await db.Humans.FirstOrDefaultAsync(p => p.Id == id);
                if (human != null)
                    return View(human);
            }
            return NotFound();
        }
     

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Human human = new Human { Id = id.Value };
                db.Entry(human).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }

}