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
    public class AdherentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Adherents
        public async Task<ActionResult> Index()
        {
            var adherents = db.Adherents.Include(a => a.EditeurCourant);
            return View(await adherents.ToListAsync());
        }

        // GET: Adherents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = await db.Adherents.FindAsync(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // GET: Adherents/Create
        public ActionResult Create()
        {
            //ViewBag.EditeurCourantId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            return View();
        }

        // POST: Adherents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AdherentId,AdherentNom,AdherentPrenom,Famille,Notes,EMail,Telephone,Adresse,Adresse2,CodePostal,Ville,DateCreation,DateModification,DateResiliation,Statut,CertificatMedical,EditeurCourantId")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Adherents.Add(adherent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.EditeurCourantId = new SelectList(db.ApplicationUsers, "Id", "FirstName", adherent.EditeurCourantId);
            return View(adherent);
        }

        // GET: Adherents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = await db.Adherents.FindAsync(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EditeurCourantId = new SelectList(db.ApplicationUsers, "Id", "FirstName", adherent.EditeurCourantId);
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AdherentId,AdherentNom,AdherentPrenom,Famille,Notes,EMail,Telephone,Adresse,Adresse2,CodePostal,Ville,DateCreation,DateModification,DateResiliation,Statut,CertificatMedical,EditeurCourantId")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adherent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.EditeurCourantId = new SelectList(db.ApplicationUsers, "Id", "FirstName", adherent.EditeurCourantId);
            return View(adherent);
        }

        // GET: Adherents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adherent adherent = await db.Adherents.FindAsync(id);
            if (adherent == null)
            {
                return HttpNotFound();
            }
            return View(adherent);
        }

        // POST: Adherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Adherent adherent = await db.Adherents.FindAsync(id);
            db.Adherents.Remove(adherent);
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
