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
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		// GET: Reglements
		public async Task<ActionResult> Index()
		{
			var reglements = _applicationDbContext.Reglements.Include(r => r.Abonnement);
			return View(await reglements.ToListAsync());
		}

		// GET: Reglements/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reglement reglement = await _applicationDbContext.Reglements.FindAsync(id);
			if (reglement == null)
			{
				return HttpNotFound();
			}
			return View(reglement);
		}

		// GET: Reglements/Create
		public ActionResult Create()
		{
			ViewBag.AbonnementId = new SelectList(_applicationDbContext.Abonnements, "AbonnementId", "AbonnementId");
			return View();
		}

		// GET: Reglements/Create
		public ActionResult CreateForAdherent(int adherentId)
		{
			var abonnementsDispoAdherent = _applicationDbContext.GetAbonnementsWithRelatedInfos()
																	.ToList();

			var abonnementslist = abonnementsDispoAdherent
																	.Select(a => new SelectListItem { Text = a.Formule.FormuleNom + " (" + a.Activite.ActiviteNom + " / " + a.Activite.Lieu.LieuNom + ")", Value = a.AbonnementId.ToString() });

			ViewBag.AbonnementId = new SelectList(abonnementslist, "Value", "Text");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "ReglementId,Montant,MoyenPaiement,IsAdhesionIncluse,ChequeNumero,ChequeBanque,ChequeTitulaire,ChequeDate,ChequeDateEncaissement,AbonnementId")] Reglement reglement)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Reglements.Add(reglement);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.AbonnementId = new SelectList(_applicationDbContext.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
			return View(reglement);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateForAdherent([Bind(Include = "ReglementId,Montant,MoyenPaiement,IsAdhesionIncluse,ChequeNumero,ChequeBanque,ChequeTitulaire,ChequeDate,ChequeDateEncaissement,AbonnementId")] Reglement reglement)
		{
			if (ModelState.IsValid)
			{
				_applicationDbContext.Reglements.Add(reglement);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.AbonnementId = new SelectList(_applicationDbContext.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
			return View(reglement);
		}

		// GET: Reglements/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reglement reglement = await _applicationDbContext.Reglements.FindAsync(id);
			if (reglement == null)
			{
				return HttpNotFound();
			}
			ViewBag.AbonnementId = new SelectList(_applicationDbContext.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
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
				_applicationDbContext.Entry(reglement).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.AbonnementId = new SelectList(_applicationDbContext.Abonnements, "AbonnementId", "AbonnementId", reglement.AbonnementId);
			return View(reglement);
		}

		// GET: Reglements/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Reglement reglement = await _applicationDbContext.Reglements.FindAsync(id);
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
			Reglement reglement = await _applicationDbContext.Reglements.FindAsync(id);
			_applicationDbContext.Reglements.Remove(reglement);
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
