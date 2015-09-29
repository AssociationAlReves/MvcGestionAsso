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
using MvcGestionAsso.Utils;
using System.Data.Entity.Infrastructure;
using MvcGestionAsso.BusinessRules;

namespace MvcGestionAsso.Controllers
{
	public class AbonnementsController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Abonnements
		public async Task<ActionResult> Index()
		{
			var abonnements = _applicationDbContext.Abonnements.Include(a => a.Adherent).Include(a => a.Formule);
			return View(await abonnements.ToListAsync());
		}

		public ActionResult IndexForAdherent(int adherentId)
		{
			var abonnements = _applicationDbContext.GetAbonnementsWithRelatedInfos()
																							.Where(a => a.AdherentId == adherentId);
			return PartialView("_IndexForAdherent", abonnements.ToList());
		}

		// GET: Abonnements/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Abonnement abonnement = await _applicationDbContext.Abonnements.FindAsync(id);
			if (abonnement == null)
			{
				return HttpNotFound();
			}
			return View(abonnement);
		}

		// GET: Abonnements/Create
		public ActionResult Create()
		{
			ViewBag.AdherentId = new SelectList(_applicationDbContext.Adherents, "AdherentId", "AdherentNom");
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom");
			ViewBag.ActiviteId = new SelectList(_applicationDbContext.Activites, "ActiviteId", "ActiviteNom");
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom");
			return View();
		}

		// GET: Abonnements/Create
		public ActionResult CreateForAdherent(int adherentId)
		{
			Abonnement abo = new Abonnement();
			abo.AdherentId = adherentId;
			abo.FormuleId = 0;
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom");
			ViewBag.ActiviteId = new SelectList(_applicationDbContext.Activites, "ActiviteId", "ActiviteNom");
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom");
			return PartialView("_CreateForAdherent", abo);
		}

		public ActionResult GetActivitesByLieu(int lieuId)
		{
			List<Activite> activites = _applicationDbContext.Activites.Where(a => a.LieuId == lieuId).ToList();

			if (activites.Any())
			{
				var result = activites.Select(a => new SelectListItem { Text = a.ActiviteNom, Value = a.ActiviteId.ToString() }).ToList();
				return Json(result, JsonRequestBehavior.AllowGet);
			}

			return null;
		}
		public ActionResult GetFormulesByActivite(int activiteId)
		{
			List<Formule> formules = _applicationDbContext.Formules.Where(f => f.ActiviteId == activiteId).ToList();

			if (formules.Any())
			{
				var result = formules.Select(a => new SelectListItem { Text = a.FormuleNom, Value = a.FormuleId.ToString() }).ToList();
				return Json(result, JsonRequestBehavior.AllowGet);
			}

			return null;
		}


		// POST: Abonnements/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "AbonnementId,AdherentId,LieuId,ActiviteId,FormuleId,TypeReglement")] Abonnement abonnement)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Abonnements.Add(abonnement);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}


			ViewBag.AdherentId = new SelectList(_applicationDbContext.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", abonnement.LieuId);
			ViewBag.ActiviteId = new SelectList(_applicationDbContext.Activites, "ActiviteId", "ActiviteNom", abonnement.ActiviteId);
			return View(abonnement);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateForAdherent([Bind(Include = "AbonnementId,AdherentId,LieuId,ActiviteId,FormuleId,TypeReglement")] Abonnement abonnement)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_applicationDbContext.Abonnements.Add(abonnement);
					await _applicationDbContext.SaveChangesAsync();
					return Json(new { success = true });
				}
				else
				{
					ModelState.TraceModelErrors();

					return Json(new
					{
						success = false,
						message = String.Join(" ", ModelState.GetModelErrors().ToArray())
					});
				}
			}
			catch (DbUpdateException ex)
			{
				return Json(new
				{
					success = false,
					message = ex.InnerException.InnerException.Message
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = ex.Message
				});
			}

		}


		// GET: Abonnements/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Abonnement abonnement = await _applicationDbContext.Abonnements.FindAsync(id);
			if (abonnement == null)
			{
				return HttpNotFound();
			}
			ViewBag.AdherentId = new SelectList(_applicationDbContext.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
			return View(abonnement);
		}

		public async Task<ActionResult> EditForAdherent(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Abonnement abonnement = await _applicationDbContext.Abonnements
																			.Include(a => a.Formule)
																			.Include(a => a.Formule.Activite)
																			.Include(a => a.Formule.Activite.Lieu)
																			.Include(a => a.Adherent)
																			.FirstAsync(a => a.AbonnementId == id);
			if (abonnement == null)
			{
				return HttpNotFound();
			}
			abonnement.LieuId = abonnement.Lieu.LieuId;
			abonnement.ActiviteId = abonnement.Activite.ActiviteId;
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
			ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", abonnement.LieuId);
			ViewBag.ActiviteId = new SelectList(_applicationDbContext.Activites, "ActiviteId", "ActiviteNom", abonnement.ActiviteId);
			return PartialView("_EditForAdherent", abonnement);
		}

		// POST: Abonnements/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "AbonnementId,AdherentId,FormuleId,TypeReglement,DateCreation")] Abonnement abonnement)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Entry(abonnement).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.TraceModelErrors();
			}
			ViewBag.AdherentId = new SelectList(_applicationDbContext.Adherents, "AdherentId", "AdherentNom", abonnement.AdherentId);
			ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
			return View(abonnement);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditForAdherent([Bind(Include = "AbonnementId,AdherentId,LieuId,ActiviteId,FormuleId,TypeReglement")] Abonnement abonnement)
		{
			//ViewBag.FormuleId = new SelectList(_applicationDbContext.Formules, "FormuleId", "FormuleNom", abonnement.FormuleId);
			//ViewBag.LieuId = new SelectList(_applicationDbContext.Lieux, "LieuId", "LieuNom", abonnement.LieuId);
			//ViewBag.ActiviteId = new SelectList(_applicationDbContext.Activites, "ActiviteId", "ActiviteNom", abonnement.ActiviteId);

			if (ModelState.IsValid)
			{
				_applicationDbContext.Entry(abonnement).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return Json(new { success = true });
			}
			else
			{
				ModelState.TraceModelErrors();

				return Json(new
				{
					success = false,
					message = String.Join(" ", ModelState.GetModelErrors().ToArray())
				});
			}
			return PartialView("_EditForAdherent", abonnement);
		}

		// GET: Abonnements/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Abonnement abonnement = await _applicationDbContext.Abonnements.FindAsync(id);
			if (abonnement == null)
			{
				return HttpNotFound();
			}
			return View(abonnement);
		}

		public async Task<ActionResult> DeleteForAdherent(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Abonnement abonnement = await _applicationDbContext.Abonnements
																			.Include(a => a.Formule)
																			.Include(a => a.Adherent)
																			.FirstAsync(a => a.AbonnementId == id);
			if (abonnement == null)
			{
				return HttpNotFound();
			}
			return PartialView("_DeleteForAdherent", abonnement);
		}

		[HttpPost, ActionName("DeleteForAdherent")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmedForAdherent(int id)
		{
			try
			{
				Abonnement abonnement = await _applicationDbContext.Abonnements.FindAsync(id);
				BusinessRuleResult result = AssoBusinessRules.CanDelete(_applicationDbContext, abonnement);

				if (result.Success)
				{
					_applicationDbContext.Abonnements.Remove(abonnement);
					await _applicationDbContext.SaveChangesAsync();
					return Json(new { success = result.Success, message = result.Message });
				}
				else
				{
					return Json(new { success = result.Success, message = result.Message });
				}
			}
			catch (DbUpdateException)
			{
				return Json(new { success = false, message = "Suppression impossible. Vérifiez si des règlements sont liés." });
			}
		}
		// POST: Abonnements/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Abonnement abonnement = await _applicationDbContext.Abonnements.FindAsync(id);
			_applicationDbContext.Abonnements.Remove(abonnement);
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
