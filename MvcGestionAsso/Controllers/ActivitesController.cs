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
using System.Data.Entity.Infrastructure;
using MvcGestionAsso.BusinessRules;

namespace MvcGestionAsso.Controllers
{
	public class ActivitesController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Activites
		public async Task<ActionResult> Index(string sort)
		{
			ViewBag.NomSort = ComputeSort("nom", sort);
			ViewBag.CodeSort = ComputeSort("code", sort);
			ViewBag.AnneeSort = ComputeSort("annee", sort, false);
			ViewBag.LieuSort = ComputeSort("lieu", sort);
			ViewBag.CategorieSort = ComputeSort("categorie", sort);

			ViewBag.CurrentSort = sort;

			IQueryable<Activite> activites = _applicationDbContext.Activites.Include(a => a.Categorie).Include(a => a.Lieu);

			#region Sorting
			switch (sort)
			{
				case "annee":
					activites = activites
												.OrderBy(a => a.DateDebut)
												.ThenByDescending(a => a.Lieu.LieuNom);
					break;

				case "nom":
					activites = activites
												.OrderBy(a => a.ActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "nom_desc":
					activites = activites
												.OrderByDescending(a => a.ActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "code":
					activites = activites
												.OrderBy(a => a.ActiviteCode)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "code_desc":
					activites = activites
												.OrderByDescending(a => a.ActiviteCode)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "lieu":
					activites = activites
												.OrderBy(a => a.Lieu.LieuNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "lieu_desc":
					activites = activites
												.OrderByDescending(a => a.Lieu.LieuNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "categorie":
					activites = activites
												.OrderBy(a => a.Categorie.CategorieActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "categorie_desc":
					activites = activites
												.OrderByDescending(a => a.Categorie.CategorieActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				default:
					activites = activites
												.OrderByDescending(a => a.DateDebut)
												.ThenByDescending(a => a.Lieu.LieuNom);
					break;
			}
			#endregion


			return View(await activites.ToListAsync());
		}

		// GET: Activites
		public ActionResult IndexForLieu(int lieuId, string sort)
		{
			ViewBag.LieuId = lieuId;

			ViewBag.NomSort = ComputeSort("nom", sort);
			ViewBag.CodeSort = ComputeSort("code", sort);
			ViewBag.AnneeSort = ComputeSort("annee", sort, false);
			ViewBag.LieuSort = ComputeSort("lieu", sort);
			ViewBag.CategorieSort = ComputeSort("categorie", sort);

			ViewBag.CurrentSort = sort;

			IQueryable<Activite> activites = _applicationDbContext.Activites
																									.Where(a => a.LieuId == lieuId)
																									.Include(a => a.Categorie).Include(a => a.Lieu);

			#region Sorting
			switch (sort)
			{
				case "annee":
					activites = activites
												.OrderBy(a => a.DateDebut)
												.ThenByDescending(a => a.Lieu.LieuNom);
					break;

				case "nom":
					activites = activites
												.OrderBy(a => a.ActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "nom_desc":
					activites = activites
												.OrderByDescending(a => a.ActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "code":
					activites = activites
												.OrderBy(a => a.ActiviteCode)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "code_desc":
					activites = activites
												.OrderByDescending(a => a.ActiviteCode)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "lieu":
					activites = activites
												.OrderBy(a => a.Lieu.LieuNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "lieu_desc":
					activites = activites
												.OrderByDescending(a => a.Lieu.LieuNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "categorie":
					activites = activites
												.OrderBy(a => a.Categorie.CategorieActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				case "categorie_desc":
					activites = activites
												.OrderByDescending(a => a.Categorie.CategorieActiviteNom)
												.ThenByDescending(a => a.DateDebut);
					break;

				default:
					activites = activites
												.OrderByDescending(a => a.DateDebut)
												.ThenByDescending(a => a.Lieu.LieuNom);
					break;
			}
			#endregion


			return PartialView("_IndexForLieu", activites.ToList());
		}

		private string ComputeSort(string fieldName, string inputSort, bool isDefault = false, bool isDefaultSortAscending = true)
		{
			string outputSort = null;
			if (isDefault)
			{
				if (isDefaultSortAscending)
				{
					outputSort = string.IsNullOrEmpty(inputSort) ? fieldName + "_desc" : string.Empty;
				}
				else
				{
					outputSort = string.IsNullOrEmpty(inputSort) ? fieldName : string.Empty;
				}
			}
			else
			{
				outputSort = inputSort == fieldName ? fieldName + "_desc" : fieldName;
			}

			return outputSort;
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
		public async Task<ActionResult> DetailsForLieu(int? id)
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
			return PartialView("_DetailsForLieu", activite);
		}

		// GET: Activites/Create
		public ActionResult Create()
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom");
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom");
			return View();
		}

		public ActionResult CreateForLieu(int lieuId)
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom");
			Activite activite = new Activite();
			activite.DateDebut = DateTime.Now;
			activite.DateFin = DateTime.Now.AddMonths(1);
			activite.LieuId = lieuId;
			return PartialView("_CreateForLieu", activite);
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

		// POST: Activites/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateForLieu([Bind(Include = "ActiviteId,ActiviteNom,ActiviteCode,DateDebut,DateFin,Planification,LieuId,CategorieActiviteId")] Activite activite)
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);

			if (ModelState.IsValid)
			{
				// check dates
				if (activite.DateFin < activite.DateDebut)
				{
					ModelState.AddModelError("DateFin", "La date de fin doit être après la date de début.");
					return Json(new { success = false, message = "La date de fin doit être après la date de début." });
				}

				_applicationDbContext.Activites.Add(activite);
				await _applicationDbContext.SaveChangesAsync();
				return Json(new { success = true });
			}

			return PartialView("_CreateForLieu", activite);
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

		public ActionResult EditForLieu(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activite activite = _applicationDbContext.Activites.Find(id);
			if (activite == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);
			return PartialView("_EditForLieu", activite);
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

		// POST: Activites/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditForLieu([Bind(Include = "ActiviteId,ActiviteNom,ActiviteCode,DateDebut,DateFin,Planification,LieuId,CategorieActiviteId")] Activite activite)
		{
			ViewBag.CategorieActiviteId = new SelectList(_applicationDbContext.CategoriesActivite, "Id", "CategorieActiviteNom", activite.CategorieActiviteId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", activite.LieuId);

			if (ModelState.IsValid)
			{
				// check dates
				if (activite.DateFin < activite.DateDebut)
				{
					ModelState.AddModelError("DateFin", "La date de fin doit être après la date de début.");
					return Json(new { success = false, message = "La date de fin doit être après la date de début." });
				}

				_applicationDbContext.Entry(activite).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return Json(new { success = true });
			}
			return PartialView("_EditForLieu", activite);
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

		public ActionResult DeleteForLieu(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Activite activite = _applicationDbContext.Activites.Find(id);
			if (activite == null)
			{
				return HttpNotFound();
			}
			return PartialView("_DeleteForLieu", activite);
		}

		// POST: Activites/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Activite activite = await _applicationDbContext.Activites.FindAsync(id);

			BusinessRuleResult result = ActiviteBR.CanDelete(_applicationDbContext, activite);

			if (result.Success)
			{
				_applicationDbContext.Activites.Remove(activite);
				await _applicationDbContext.SaveChangesAsync();
				return Json(new { success = result.Success, message = result.Message });
			}
			else
			{
				return Json(new { success = result.Success, message = result.Message });
			}

		}

		// POST: Activites/Delete/5
		[HttpPost, ActionName("DeleteForLieu")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmedForLieu(int id)
		{
			try
			{
				Activite activite = await _applicationDbContext.Activites.FindAsync(id);

				BusinessRuleResult result = ActiviteBR.CanDelete(_applicationDbContext, activite);

				if (result.Success)
				{
					_applicationDbContext.Activites.Remove(activite);
					await _applicationDbContext.SaveChangesAsync();
					return Json(new { success = result.Success, message = result.Message });
				}
				else
				{
					return Json(new { success = result.Success, message = result.Message });
				}	
			}
			catch (DbUpdateException ex)
			{
				return Json(new { success = false, message = "Suppression impossible. Vérifiez si des Formules sont liées." });
			}

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
