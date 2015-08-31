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
    public class LieuxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lieux
        public async Task<ActionResult> Index()
        {
            return View(await db.Lieux.ToListAsync());
        }

        // GET: Lieux/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieu lieu = await db.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // GET: Lieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lieux/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LieuId,LieuCode,LieuNom,Adresse,Adresse2,CodePostal,Ville")] Lieu lieu)
        {
            if (ModelState.IsValid)
            {
                db.Lieux.Add(lieu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lieu);
        }

        // GET: Lieux/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieu lieu = await db.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // POST: Lieux/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LieuId,LieuCode,LieuNom,Adresse,Adresse2,CodePostal,Ville")] Lieu lieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lieu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lieu);
        }

        // GET: Lieux/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieu lieu = await db.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }

        // POST: Lieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lieu lieu = await db.Lieux.FindAsync(id);
            db.Lieux.Remove(lieu);
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
