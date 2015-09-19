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
    public class ReglementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reglements
        public async Task<ActionResult> Index()
        {
            var reglements = db.Reglements.Include(r => r.Abonnement);
            return View(await reglements.ToListAsync());
        }

        // GET: Reglements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reglement reglement = await db.Reglements.FindAsync(id);
            if (reglement == null)
            {
                return HttpNotFound();
            }
            return View(reglement);
        }

        // GET: Reglements/Create
        public ActionResult Create()
        {
            ViewBag.AbonnementId = new SelectList(db.Abonnements, "AbonnementId", "AbonnementId");
            return View();
        }

        // POST: Reglements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReglementId,Montant,MoyenPaiement,IsAdhesionIncluse,ChequeNumero,ChequeBanque,ChequeTitulaire,ChequeDate,ChequeDateEncaissement,AbonnementId")] Reglement reglement)
        {
            if (ModelState.IsValid)
            {
                db.Reglements.Add(reglement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AbonnementId = new SelectList(db.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
            return View(reglement);
        }

        // GET: Reglements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reglement reglement = await db.Reglements.FindAsync(id);
            if (reglement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AbonnementId = new SelectList(db.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
            return View(reglement);
        }

        // POST: Reglements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReglementId,Montant,MoyenPaiement,IsAdhesionIncluse,ChequeNumero,ChequeBanque,ChequeTitulaire,ChequeDate,ChequeDateEncaissement,AbonnementId")] Reglement reglement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reglement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AbonnementId = new SelectList(db.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
            return View(reglement);
        }

        // GET: Reglements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reglement reglement = await db.Reglements.FindAsync(id);
            if (reglement == null)
            {
                return HttpNotFound();
            }
            return View(reglement);
        }

        // POST: Reglements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reglement reglement = await db.Reglements.FindAsync(id);
            db.Reglements.Remove(reglement);
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
