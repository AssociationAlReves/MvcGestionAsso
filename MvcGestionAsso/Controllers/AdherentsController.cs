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
using PagedList;
using PagedList.Mvc;

namespace MvcGestionAsso.Controllers
{
	public class AdherentsController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Adherents
		public ActionResult Index(string sort, string search, int? page)
		{
			ViewBag.NomSort = ComputeSort("nom", sort, true);
			ViewBag.PrenomSort = ComputeSort("prenom", sort);
			ViewBag.FamilleSort = ComputeSort("famille", sort);
			ViewBag.DateCreationSort = ComputeSort("datecreation", sort);
			ViewBag.DateModifSort = ComputeSort("datemodif", sort);
			ViewBag.StatutSort = ComputeSort("statut", sort);

			ViewBag.CurrentSort = sort;
			ViewBag.CurrentSearch = search;

			IQueryable<Adherent> adherents = _applicationDbContext.Adherents;

			#region Searching
			if (!String.IsNullOrEmpty(search))
			{
				adherents = adherents
					.Where(a => a.AdherentNom.StartsWith(search) || a.AdherentPrenom.StartsWith(search));
			}
			#endregion

			#region Sorting
			switch (sort)
			{
				case "nom_desc":
					adherents = adherents
												.OrderByDescending(a => a.AdherentNom)
												.ThenByDescending(a => a.DateCreation);
					break;

				case "prenom":
					adherents = adherents
												.OrderBy(a => a.AdherentPrenom);
					break;

				case "prenom_desc":
					adherents = adherents
												.OrderByDescending(a => a.AdherentPrenom);
					break;

				case "famille":
					adherents = adherents
												.OrderBy(a => a.Famille);
					break;

				case "famille_desc":
					adherents = adherents
												.OrderByDescending(a => a.Famille);
					break;

				case "datecreation":
					adherents = adherents
												.OrderBy(a => a.DateCreation);
					break;

				case "datecreation_desc":
					adherents = adherents
												.OrderByDescending(a => a.DateCreation);
					break;

				case "datemodif":
					adherents = adherents
												.OrderBy(a => a.DateModification);
					break;

				case "datemodif_desc":
					adherents = adherents
												.OrderByDescending(a => a.DateModification);
					break;

				case "statut":
					adherents = adherents
												.OrderBy(a => a.Statut);
					break;

				case "statut_desc":
					adherents = adherents
												.OrderByDescending(a => a.Statut);
					break;

				default:
					adherents = adherents
												.OrderBy(a => a.AdherentNom)
												.ThenByDescending(a => a.DateCreation);
					break;
			}
			#endregion

			#region Pagination
			const int PAGE_SIZE = 10;
			int pageNumber = page ?? 1;
			#endregion

			return View(adherents.ToPagedList(pageNumber, PAGE_SIZE));
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

		// GET: Adherents/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Adherent adherent = await _applicationDbContext.Adherents.FindAsync(id);
			if (adherent == null)
			{
				return HttpNotFound();
			}
			return View(adherent);
		}

		// GET: Adherents/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Adherents/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "AdherentId,AdherentNom,AdherentPrenom,Famille,Notes,EMail,Telephone,Adresse,Adresse2,CodePostal,Ville,Statut,CertificatMedical")] Adherent adherent)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Adherents.Add(adherent);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(adherent);
		}

		// GET: Adherents/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Adherent adherent = await _applicationDbContext.Adherents.FindAsync(id);
			if (adherent == null)
			{
				return HttpNotFound();
			}
			return View(adherent);
		}

		// POST: Adherents/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "AdherentId,AdherentNom,AdherentPrenom,Famille,Notes,EMail,Telephone,Adresse,Adresse2,CodePostal,Ville,Statut,DateCreation,CertificatMedical")] Adherent adherent)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Entry(adherent).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(adherent);
		}

		// GET: Adherents/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Adherent adherent = await _applicationDbContext.Adherents.FindAsync(id);
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
			Adherent adherent = await _applicationDbContext.Adherents.FindAsync(id);
			_applicationDbContext.Adherents.Remove(adherent);
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
