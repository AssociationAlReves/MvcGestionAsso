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
using TreeUtility;
using MvcGestionAsso.ViewModels;
using System.Data.Entity.Infrastructure;

namespace MvcGestionAsso.Controllers
{
	public class CategoriesActiviteController : Controller
	{
		private ApplicationDbContext _applicationDbContext = new ApplicationDbContext();

		private List<CategorieActivite> GetListOfNodes()
		{
			List<CategorieActivite> sourceCategories = _applicationDbContext.CategoriesActivite.ToList();
			List<CategorieActivite> categories = new List<CategorieActivite>();
			foreach (CategorieActivite sourceCategorie in sourceCategories)
			{
				CategorieActivite c = new CategorieActivite();
				c.Id = sourceCategorie.Id;
				c.CategorieActiviteNom = sourceCategorie.CategorieActiviteNom;
				if (sourceCategorie.ParentId != null)
				{
					c.Parent = new CategorieActivite();
					c.Parent.Id = (int)sourceCategorie.ParentId;
				}
				categories.Add(c);
			}
			return categories;
		}

		private string EnumerateNodes(CategorieActivite parent)
		{
			// Init an empty string
			string content = String.Empty;

			// Add <li> categorie name
			content += "<li class=\"treenode\">";
			content += parent.CategorieActiviteNom;
			content += String.Format("<a href=\"/CategoriesActivite/Edit/{0}\" class=\"btn btn-primary btn-xs treenodeeditbutton\">Modifier</a>", parent.Id);
			content += String.Format("<a href=\"/CategoriesActivite/Delete/{0}\" class=\"btn btn-danger btn-xs treenodedeletebutton\">Supprimer</a>", parent.Id);

			// If there are no children, end the </li>
			if (parent.Children.Count == 0)
				content += "</li>";
			else   // If there are children, start a <ul>
				content += "<ul>";

			// Loop one past the number of children
			int numberOfChildren = parent.Children.Count;
			for (int i = 0; i <= numberOfChildren; i++)
			{
				// If this iteration's index points to a child,
				// call this function recursively
				if (numberOfChildren > 0 && i < numberOfChildren)
				{
					CategorieActivite child = parent.Children[i];
					content += EnumerateNodes(child);
				}

				// If this iteration's index points past the children, end the </ul>
				if (numberOfChildren > 0 && i == numberOfChildren)
					content += "</ul>";
			}

			// Return the content
			return content;
		}


		private void ValidateParentsAreParentless(CategorieActivite categorie)
		{
			// There is no parent
			if (categorie.ParentId == null)
				return;

			// The parent has a parent
			CategorieActivite parentCategory = _applicationDbContext.CategoriesActivite.Find(categorie.ParentId);
			if (parentCategory.ParentId != null)
				throw new InvalidOperationException("Seuls deux niveaux hiérarchiques sont autorisés.");

			// The parent does NOT have a parent, but the categorie being nested has children
			int numberOfChildren = _applicationDbContext.CategoriesActivite.Count(c => c.ParentId== categorie.Id);
			if (numberOfChildren > 0)
				throw new InvalidOperationException("Seuls deux niveaux hiérarchiques sont autorisés.");
		}


		private SelectList PopulateParentCategorySelectList(int? id)
		{
			SelectList selectList;

			if (id == null)
				selectList = new SelectList(
						_applicationDbContext
						.CategoriesActivite
						.Where(c => c.ParentId == null), "Id", "CategorieActiviteNom");
			else if (_applicationDbContext.CategoriesActivite.Count(c => c.ParentId == id) == 0)
				selectList = new SelectList(
						_applicationDbContext
						.CategoriesActivite
						.Where(c => c.ParentId == null && c.Id != id), "Id", "CategorieActiviteNom");
			else
				selectList = new SelectList(
						_applicationDbContext
						.CategoriesActivite
						.Where(c => false), "Id", "CategorieActiviteNom");

			return selectList;
		}

		public ActionResult Index()
		{
			// Start the outermost list
			string fullString = "<ul>";

			IList<CategorieActivite> listOfNodes = GetListOfNodes();
			IList<CategorieActivite> topLevelCategories = TreeHelper.ConvertToForest(listOfNodes);

			foreach (var category in topLevelCategories)
				fullString += EnumerateNodes(category);

			// End the outermost list
			fullString += "</ul>";

			return View((object)fullString);
		}

		public ActionResult Create()
		{
			ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,ParentId,CategorieActiviteNom")] CategorieActivite category)
		{
			if (ModelState.IsValid)
			{
				try
				{
					ValidateParentsAreParentless(category);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
					ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
					return View(category);
				}


				_applicationDbContext.CategoriesActivite.Add(category);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
			return View(category);
		}


		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CategorieActivite category = await _applicationDbContext.CategoriesActivite.FindAsync(id);
			if (category == null)
			{
				return HttpNotFound();
			}

			// Wind-up a Category viewmodel
			CategorieActiviteViewModel categoryViewModel = new CategorieActiviteViewModel();
			categoryViewModel.Id = category.Id;
			categoryViewModel.ParentId = category.ParentId;
			categoryViewModel.CategorieActiviteNom = category.CategorieActiviteNom;
			
			ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(categoryViewModel.Id);
			return View(categoryViewModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,ParentId,CategorieActiviteNom")] CategorieActiviteViewModel categoryViewModel)
		{
			if (ModelState.IsValid)
			{
				// Unwind back to a Category
				CategorieActivite editedCategory = new CategorieActivite();

				try
				{
					editedCategory.Id = categoryViewModel.Id;
					editedCategory.ParentId = categoryViewModel.ParentId;
					editedCategory.CategorieActiviteNom = categoryViewModel.CategorieActiviteNom;
					ValidateParentsAreParentless(editedCategory);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
					ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(categoryViewModel.Id);
					return View("Edit", categoryViewModel);
				}


				_applicationDbContext.Entry(editedCategory).State = EntityState.Modified;
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(categoryViewModel.Id);
			return View(categoryViewModel);
		}


		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CategorieActivite category = await _applicationDbContext.CategoriesActivite.FindAsync(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			CategorieActivite category = await _applicationDbContext.CategoriesActivite.FindAsync(id);

			try
			{
				_applicationDbContext.CategoriesActivite.Remove(category);
				await _applicationDbContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("", "Vous ne pouvez pas supprimer une catégorie qui a des sous-catégories.");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return View("Delete", category);
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
