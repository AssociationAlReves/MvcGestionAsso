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
    public class IntervenantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Intervenants
        public async Task<ActionResult> Index()
        {
            return View(await db.Intervenants.ToListAsync());
        }

        // GET: Intervenants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervenant intervenant = await db.Intervenants.FindAsync(id);
            if (intervenant == null)
            {
                return HttpNotFound();
            }
            return View(intervenant);
        }

        // GET: Intervenants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Intervenants/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IntervenantId,IntervenantNom,IntervenantPrenom,NumeroSecuriteSociale,DateCreation,DateModification")] Intervenant intervenant)
        {
            if (ModelState.IsValid)
            {
                db.Intervenants.Add(intervenant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(intervenant);
        }

        // GET: Intervenants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervenant intervenant = await db.Intervenants.FindAsync(id);
            if (intervenant == null)
            {
                return HttpNotFound();
            }
            return View(intervenant);
        }

        // POST: Intervenants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IntervenantId,IntervenantNom,IntervenantPrenom,NumeroSecuriteSociale,DateCreation,DateModification")] Intervenant intervenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intervenant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(intervenant);
        }

        // GET: Intervenants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Intervenant intervenant = await db.Intervenants.FindAsync(id);
            if (intervenant == null)
            {
                return HttpNotFound();
            }
            return View(intervenant);
        }

        // POST: Intervenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Intervenant intervenant = await db.Intervenants.FindAsync(id);
            db.Intervenants.Remove(intervenant);
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
