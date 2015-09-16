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
	public class ActivitesController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Activites
		public async Task<ActionResult> Index()
		{
			var activites = _applicationDbContext.Activites.Include(a => a.Categorie).Include(a => a.Lieu);
			return View(await activites.ToListAsync());
		}

		// GET: Activites/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activite activite = await _applicationDbContext.Activites.FindAsync(id);
			if (activite == null)
			{
				return HttpNotFound();
			}
			return View(activite);
		}

		// GET: Activites/Create
		public ActionResult Create()
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom");
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom");
			return View();
		}

		// POST: Activites/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "ActiviteId,ActiviteNom,ActiviteCode,DateDebut,DateFin,Planification,LieuId,CategorieActiviteId")] Activite activite)
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);

			if (ModelState.IsValid)
			{
				// check dates
				if (activite.DateFin < activite.DateDebut)
				{
					ModelState.AddModelError("DateFin", "La date de fin doit être après la date de début.");
					return View(activite);
				}

				_applicationDbContext.Activites.Add(activite);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(activite);
		}

		// GET: Activites/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activite activite = await _applicationDbContext.Activites.FindAsync(id);
			if (activite == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);
			return View(activite);
		}

		// POST: Activites/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "ActiviteId,ActiviteNom,ActiviteCode,DateDebut,DateFin,Planification,LieuId,CategorieActiviteId")] Activite activite)
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);

			if (ModelState.IsValid)
			{
				// check dates
				if (activite.DateFin < activite.DateDebut)
				{
					ModelState.AddModelError("DateFin", "La date de fin doit être après la date de début.");
					return View(activite);
				}

				_applicationDbContext.Entry(activite).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(activite);
		}

		// GET: Activites/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activite activite = await _applicationDbContext.Activites.FindAsync(id);
			if (activite == null)
			{
				return HttpNotFound();
			}
			return View(activite);
		}

		// POST: Activites/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Activite activite = await _applicationDbContext.Activites.FindAsync(id);
			_applicationDbContext.Activites.Remove(activite);
			await _applicationDbContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_applicationDbContext.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
