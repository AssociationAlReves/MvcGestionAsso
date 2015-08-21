namespace MvcGestionAsso.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using MvcGestionAsso.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<MvcGestionAsso.Models.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(MvcGestionAsso.Models.ApplicationDbContext context)
		{
			// --------------------------------------------------------------------
			// CategorieActivite
			// ---
			string nomCatDanseContemporaine = "Danse contemporaine";
			context.CategoriesActivite.AddOrUpdate(c => c.CategorieActiviteNom,
				 new CategorieActivite
				 {
					 CategorieActiviteNom = nomCatDanseContemporaine
				 });

			string nomCatZumba = "Zumba";
			context.CategoriesActivite.AddOrUpdate(c => c.CategorieActiviteNom,
				 new CategorieActivite
				 {
					 CategorieActiviteNom = nomCatZumba
				 });
			context.SaveChanges();
			CategorieActivite catZumba = context.CategoriesActivite.First(c => c.CategorieActiviteNom == nomCatZumba);
			CategorieActivite catDanse = context.CategoriesActivite.First(c => c.CategorieActiviteNom == nomCatDanseContemporaine);

			// --------------------------------------------------------------------
			// Lieux
			// ---
			string lieuCDH = "CDH";
			context.Lieux.AddOrUpdate(l => l.LieuCode,
				 new Lieu
				 {
					 LieuNom = "Château de l'Horloge",
					 LieuCode = lieuCDH,
					 Adresse = "50 Place Du Château de l'Horloge",
					 Adresse2 = "Jas de Bouffan",
					 CodePostal = "13090",
					 Ville = "Aix-en-Provence"
				 });

			string lieuBEL = "BEL";
			context.Lieux.AddOrUpdate(l => l.LieuCode,
				 new Lieu
				 {
					 LieuNom = "Espace Jeunesse Bellegarde",
					 LieuCode = lieuBEL,
					 Adresse = "37 Boulevard Aristide Briand",
					 CodePostal = "13100",
					 Ville = "Aix-en-Provence"
				 });

			context.SaveChanges();
			Lieu cdh = context.Lieux.First(l => l.LieuCode == lieuCDH);
			Lieu bel = context.Lieux.First(l => l.LieuCode == lieuBEL);

			// --------------------------------------------------------------------
			// Activités
			// ---
			context.Activites.AddOrUpdate(a => a.ActiviteCode,
				new Activite
				{
					ActiviteCode = "DANSE-BEL-1",
					ActiviteNom = "Danse contemporaine 6-8 ans Bellegarde",
					CategorieActiviteId = catDanse.CategorieActiviteId,
					DureeHeures = 1,
					DateDebut = new DateTime(2014, 09, 04, 17, 0, 0),
					DateFin = new DateTime(2015, 07, 01),
					LieuId = bel.LieuId,
				});

			context.Activites.AddOrUpdate(a => a.ActiviteCode,
				new Activite
				{
					ActiviteCode = "DANSE-BEL-2",
					ActiviteNom = "Danse contemporaine 8-12 ans Bellegarde",
					CategorieActiviteId = catDanse.CategorieActiviteId,
					DureeHeures = 1,
					DateDebut = new DateTime(2014, 09, 04, 18, 0, 0),
					DateFin = new DateTime(2015, 07, 01),
					LieuId = bel.LieuId,
				});

			context.Activites.AddOrUpdate(a => a.ActiviteCode,
				new Activite
				{
					ActiviteCode = "ZUMBA-BEL",
					ActiviteNom = "Zumba Bellegarde",
					CategorieActiviteId = catZumba.CategorieActiviteId,
					DureeHeures = 1,
					DateDebut = new DateTime(2014, 09, 04, 20, 0, 0),
					DateFin = new DateTime(2015, 07, 01),
					LieuId = bel.LieuId,
				});

			context.Activites.AddOrUpdate(a => a.ActiviteCode,
			new Activite
			{
				ActiviteCode = "ZUMBA-CDH",
				ActiviteNom = "Zumba",
				CategorieActiviteId = catZumba.CategorieActiviteId,
				DureeHeures = 1,
				DateDebut = new DateTime(2014, 09, 01, 19, 0, 0),
				DateFin = new DateTime(2015, 07, 01),
				LieuId = cdh.LieuId,
			});
			context.SaveChanges();
		}
	}
}
