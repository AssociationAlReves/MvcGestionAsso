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
    public class MissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Missions
        public async Task<ActionResult> Index()
        {
            var missions = db.Missions.Include(m => m.Activite).Include(m => m.Intervenant);
            return View(await missions.ToListAsync());
        }

        // GET: Missions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = await db.Missions.FindAsync(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // GET: Missions/Create
        public ActionResult Create()
        {
            ViewBag.ActiviteId = new SelectList(db.Activites, "ActiviteId", "ActiviteNom");
            ViewBag.IntervenantId = new SelectList(db.Intervenants, "IntervenantId", "IntervenantNom");
            return View();
        }

        // POST: Missions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MissionId,Description,Notes,SalaireHoraire,DateDebut,DateFin,IntervenantId,ActiviteId")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Missions.Add(mission);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActiviteId = new SelectList(db.Activites, "ActiviteId", "ActiviteNom", mission.ActiviteId);
            ViewBag.IntervenantId = new SelectList(db.Intervenants, "IntervenantId", "IntervenantNom", mission.IntervenantId);
            return View(mission);
        }

        // GET: Missions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = await db.Missions.FindAsync(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActiviteId = new SelectList(db.Activites, "ActiviteId", "ActiviteNom", mission.ActiviteId);
            ViewBag.IntervenantId = new SelectList(db.Intervenants, "IntervenantId", "IntervenantNom", mission.IntervenantId);
            return View(mission);
        }

        // POST: Missions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MissionId,Description,Notes,SalaireHoraire,DateDebut,DateFin,IntervenantId,ActiviteId")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActiviteId = new SelectList(db.Activites, "ActiviteId", "ActiviteNom", mission.ActiviteId);
            ViewBag.IntervenantId = new SelectList(db.Intervenants, "IntervenantId", "IntervenantNom", mission.IntervenantId);
            return View(mission);
        }

        // GET: Missions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = await db.Missions.FindAsync(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mission mission = await db.Missions.FindAsync(id);
            db.Missions.Remove(mission);
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
