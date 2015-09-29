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
	public class FormulesController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Formules
		public async Task<ActionResult> Index()
		{
			var formules = _applicationDbContext.Formules.Include(f => f.Activite);
			return View(await formules.ToListAsync());
		}

		// GET: Formules/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Formule formule = await _applicationDbContext.Formules.FindAsync(id);
			if (formule == null)
			{
				return HttpNotFound();
			}
			return View(formule);
		}

		// GET: Formules/Create
		public ActionResult Create()
		{
			var activites = GetListActivitesWithLieu();
			ViewBag.ActiviteId = new SelectList(activites, "Value","Text");
			return View();
		}

		// POST: Formules/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "FormuleId,FormuleNom,IsActive,Tarif,ActiviteId")] Formule formule)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Formules.Add(formule);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			var activites = GetListActivitesWithLieu();
			ViewBag.ActiviteId = new SelectList(activites, formule.ActiviteId);
			return View(formule);
		}

		List<SelectListItem> GetListActivitesWithLieu()
		{
			return _applicationDbContext.Activites.Include(a => a.Lieu)
										.Select(activite => new SelectListItem { Text = activite.ActiviteNom + " (" + activite.Lieu.LieuNom + ")", Value = activite.ActiviteId.ToString() })
										.ToList();
		}

		// GET: Formules/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Formule formule = await _applicationDbContext.Formules.FindAsync(id);
			if (formule == null)
			{
				return HttpNotFound();
			}
			var activites = GetListActivitesWithLieu();

			ViewBag.ActiviteId = new SelectList(activites, formule.ActiviteId);
			return View(formule);
		}

		// POST: Formules/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "FormuleId,FormuleNom,DebutValidite,FinValidite,IsActive,Tarif,ActiviteId")] Formule formule)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Entry(formule).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			var activites = GetListActivitesWithLieu();
			ViewBag.ActiviteId = new SelectList(activites, formule.ActiviteId);
			return View(formule);
		}

		// GET: Formules/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Formule formule = await _applicationDbContext.Formules.FindAsync(id);
			if (formule == null)
			{
				return HttpNotFound();
			}
			return View(formule);
		}

		// POST: Formules/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Formule formule = await _applicationDbContext.Formules.FindAsync(id);
			_applicationDbContext.Formules.Remove(formule);
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
