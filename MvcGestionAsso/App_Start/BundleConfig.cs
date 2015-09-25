using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;
using MvcGestionAsso.Utils;

namespace MvcGestionAsso
{
	public class BundleConfig
	{
		// Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
									"~/Scripts/jquery-{version}.js",
									"~/Scripts/jquery-ui-{version}.js",
									"~/Scripts/jquery.ui.datepicker-fr.js",
									"~/Scripts/jquery.timepicker.js",
									"~/Scripts/jquery.datepair.js"));

			var jQueryValBundel = new ScriptBundle("~/bundles/jqueryval").Include(
									"~/Scripts/globalize/globalize.js",
									"~/Scripts/globalize/cultures/globalize.culture.fr-FR.js",
									"~/Scripts/jquery.validate.js",
									"~/Scripts/jquery.validate.globalize.js",
									"~/Scripts/jquery.validate.unobtrusive.js");
			jQueryValBundel.Orderer = new AsIsBundleOrderer();
			bundles.Add(jQueryValBundel);
			
			// Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
			// prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
									"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
								"~/Scripts/bootstrap.js",
								"~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
								"~/Content/bootstrap-themes/bootstrap-yeti.css",
								"~/Content/site.css",
								"~/Content/jquery.timepicker.css"));
		}
	}
	class AsIsBundleOrderer : IBundleOrderer
	{
		public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			return files;
		}
	}
}
