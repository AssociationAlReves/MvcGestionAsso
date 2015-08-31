using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.Controllers
{
    public class CategoriesActiviteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategorieActivites
        public async Task<ActionResult> Index()
        {
            var categoriesActivite = db.CategoriesActivite.Include(c => c.Parent);
            return View(await categoriesActivite.ToListAsync());
        }

        // GET: CategorieActivites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategoriesActivite.FindAsync(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.CategoriesActivite, "Id", "CategorieActiviteNom");
            return View();
        }

        // POST: CategorieActivites/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ParentId,CategorieActiviteNom")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                db.CategoriesActivite.Add(categorieActivite);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.CategoriesActivite, "Id", "CategorieActiviteNom", categorieActivite.ParentId);
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategoriesActivite.FindAsync(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.CategoriesActivite, "Id", "CategorieActiviteNom", categorieActivite.ParentId);
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ParentId,CategorieActiviteNom")] CategorieActivite categorieActivite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorieActivite).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.CategoriesActivite, "Id", "CategorieActiviteNom", categorieActivite.ParentId);
            return View(categorieActivite);
        }

        // GET: CategorieActivites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategorieActivite categorieActivite = await db.CategoriesActivite.FindAsync(id);
            if (categorieActivite == null)
            {
                return HttpNotFound();
            }
            return View(categorieActivite);
        }

        // POST: CategorieActivites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategorieActivite categorieActivite = await db.CategoriesActivite.FindAsync(id);
            db.CategoriesActivite.Remove(categorieActivite);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
