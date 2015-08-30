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

namespace MvcGestionAsso.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ApplicationUsersController : Controller
	{
		public ApplicationUsersController()
		{
		}

		public ApplicationUsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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


		// GET: ApplicationUsers
		public async Task<ActionResult> Index()
		{
			return View(await UserManager.Users.ToListAsync());
		}

		//// GET: ApplicationUsers/Details/5
		//public async Task<ActionResult> Details(string id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
		//	if (applicationUser == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(applicationUser);
		//}

		//// GET: ApplicationUsers/Create
		//public ActionResult Create()
		//{
		//	return View();
		//}

		//// POST: ApplicationUsers/Create
		//// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		//// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Address,City,ZipCode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.ApplicationUsers.Add(applicationUser);
		//		await db.SaveChangesAsync();
		//		return RedirectToAction("Index");
		//	}

		//	return View(applicationUser);
		//}

		// GET: ApplicationUsers/Edit/5
		public async Task<ActionResult> Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
			if (applicationUser == null)
			{
				return HttpNotFound();
			}

			var userRoles = await UserManager.GetRolesAsync(applicationUser.Id);
			applicationUser.RolesList = RoleManager.Roles.ToList().Select(r => new SelectListItem
																																							{
																																								Selected = userRoles.Contains(r.Name),
																																								Text = r.Name,
																																								Value = r.Name
																																							});

			return View(applicationUser);
		}

		// POST: ApplicationUsers/Edit/5
		// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
		// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id")] ApplicationUser applicationUser, params string[] rolesSelectedOnView)
		{
			if (ModelState.IsValid)
			{
				// If the user is an admin
				var rolesCurrentlyPersisted = await UserManager.GetRolesAsync(applicationUser.Id);
				bool isAdmin = rolesCurrentlyPersisted.Contains("Admin");

				// and the user did not have Admin role checked
				rolesSelectedOnView = rolesSelectedOnView ?? new string[] { };
				bool isAdminDeselected = !rolesSelectedOnView.Contains("Admin");

				// and the current stored count of users with admin == 1
				var role = await RoleManager.FindByNameAsync("Admin");
				bool isOnlyOneAdmin = role.Users.Count == 1;

				// Populate roles list in case we have to return to edit view
				applicationUser = await UserManager.FindByIdAsync(applicationUser.Id);
				applicationUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem
																																						{
																																							Selected = rolesCurrentlyPersisted.Contains(x.Name),
																																							Text = x.Name,
																																							Value = x.Name
																																						});
				// Then prevent removal of admin role
				if (isAdmin && isAdminDeselected && isOnlyOneAdmin)
				{
					ModelState.AddModelError("", "Au moins un utilisateur doit avoir le rôle Admin. Vous ne pouvez pas supprimer le dernier utilisateur dans ce cas.");
					return View(applicationUser);
				}

				var result = await UserManager.AddToRolesAsync(
					applicationUser.Id,
					rolesSelectedOnView.Except(rolesCurrentlyPersisted).ToArray());

				if (!result.Succeeded)
				{
					ModelState.AddModelError("", result.Errors.First());
					return View(applicationUser);
				}

				result = await UserManager.RemoveFromRolesAsync(
					applicationUser.Id,
					rolesCurrentlyPersisted.Except(rolesSelectedOnView).ToArray());

				if (!result.Succeeded)
				{
					ModelState.AddModelError("", result.Errors.First());
					return View(applicationUser);
				}

				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", "Une erreur est survenue.");
			return View(applicationUser);
		}

		public async Task<ActionResult> LockAccount([Bind(Include = "Id")] string id)
		{
			await UserManager.ResetAccessFailedCountAsync(id);
			await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(100));
			return RedirectToAction("Index");
		}


		public async Task<ActionResult> UnlockAccount([Bind(Include = "Id")] string id)
		{
			await UserManager.ResetAccessFailedCountAsync(id);
			await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(-1));
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				UserManager.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
