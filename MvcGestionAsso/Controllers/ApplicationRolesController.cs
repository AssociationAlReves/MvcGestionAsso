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
using Microsoft.AspNet.Identity.Owin;
using MvcGestionAsso.ViewModels;

namespace MvcGestionAsso.Controllers
{
	//[Authorize(Roles = "Admin")]
	public class ApplicationRolesController : Controller
	{
		//private ApplicationDbContext db = new ApplicationDbContext();



		// GET: ApplicationRoles

		public ApplicationRolesController()
		{
		}

		public ApplicationRolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
		{
			UserManager = userManager;
			RoleManager = roleManager;
		}


		private ApplicationRoleManager _roleManager { get; set; }
		public ApplicationRoleManager RoleManager
		{
			get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
			set { _roleManager = value; }
		}

		private ApplicationUserManager _userManager { get; set; }
		public ApplicationUserManager UserManager
		{
			get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
			set { _userManager = value; }
		}

		public async Task<ActionResult> Index()
		{
			return View(await RoleManager.Roles.ToListAsync());
		}

		// GET: ApplicationRoles/Details/5
		public async Task<ActionResult> Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
			if (applicationRole == null)
			{
				return HttpNotFound();
			}
			return View(applicationRole);
		}

		// GET: ApplicationRoles/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ApplicationRoles/Create
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Name")] ApplicationRoleViewModel applicationRoleViewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationRole appRole = new ApplicationRole(applicationRoleViewModel.Name);
				var roleResult = await RoleManager.CreateAsync(appRole);

				if (!roleResult.Succeeded)
				{
					ModelState.AddModelError("", roleResult.Errors.First());
					return View();
				}

				return RedirectToAction("Index");
			}

			return View(applicationRoleViewModel);
		}

		// GET: ApplicationRoles/Edit/5
		public async Task<ActionResult> Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
			if (applicationRole == null)
			{
				return HttpNotFound();
			}
			ApplicationRoleViewModel appRoleViewModel = new ApplicationRoleViewModel { Id = applicationRole.Id, Name = applicationRole.Name };
			return View(appRoleViewModel);
		}

		// POST: ApplicationRoles/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ApplicationRoleViewModel applicationRoleViewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationRole retrievedAppRole = await RoleManager.FindByIdAsync(applicationRoleViewModel.Id);
				string originalName = retrievedAppRole.Name;

				if (originalName == "Admin" && applicationRoleViewModel.Name != "Admin")
				{
					ModelState.AddModelError("", "Vous ne pouvez pas renommer le rôle Admin.");
					return View(applicationRoleViewModel);
				}
				if (originalName != "Admin" && applicationRoleViewModel.Name == "Admin")
				{
					ModelState.AddModelError("", "Vous ne pouvez pas donner comme nom Admin pour un rôle.");
					return View(applicationRoleViewModel);
				}

				retrievedAppRole.Name = applicationRoleViewModel.Name;
				await RoleManager.UpdateAsync(retrievedAppRole);

				return RedirectToAction("Index");
			}
			return View(applicationRoleViewModel);
		}

		// GET: ApplicationRoles/Delete/5
		public async Task<ActionResult> Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
			if (applicationRole == null)
			{
				return HttpNotFound();
			}
			return View(applicationRole);
		}

		// POST: ApplicationRoles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id)
		{
			ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
			if (applicationRole.Name == "Admin")
			{
				ModelState.AddModelError("", "Vous ne pouvez pas supprimer le rôle Admin.");
				return View(applicationRole);
			}

			await RoleManager.DeleteAsync(applicationRole);
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				RoleManager.Dispose();
			}
			base.Dispose(disposing);
		}

	}
}
