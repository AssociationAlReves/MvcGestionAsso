using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.Migrations.Seed
{
	public static class SampleDataSeed
	{
		public static void Seed(MvcGestionAsso.Models.ApplicationDbContext context)
		{
			#region CategorieActivite
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
			#endregion

			CategorieActivite catZumba = context.CategoriesActivite.First(c => c.CategorieActiviteNom == nomCatZumba);
			CategorieActivite catDanse = context.CategoriesActivite.First(c => c.CategorieActiviteNom == nomCatDanseContemporaine);

			#region Lieux
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
			#endregion
			Lieu cdh = context.Lieux.First(l => l.LieuCode == lieuCDH);
			Lieu bel = context.Lieux.First(l => l.LieuCode == lieuBEL);

			#region Activités
			// --------------------------------------------------------------------
			// Activités
			// ---
			context.Activites.AddOrUpdate(a => a.ActiviteCode,
				new Activite
				{
					ActiviteCode = "DANSE-BEL-1",
					ActiviteNom = "Danse contemporaine 4-6 ans Bellegarde",
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
					ActiviteNom = "Danse contemporaine 7-9 ans Bellegarde",
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
			#endregion
			Activite zumbaCDH = context.Activites.First(a => a.ActiviteCode == "ZUMBA-CDH");
			Activite zumbaBEL = context.Activites.First(a => a.ActiviteCode == "ZUMBA-BEL");
			Activite danseBEL1 = context.Activites.First(a => a.ActiviteCode == "DANSE-BEL-1");
			Activite danseBEL2 = context.Activites.First(a => a.ActiviteCode == "DANSE-BEL-2");

			#region Formules
			// --------------------------------------------------------------------
			// Formules
			// ---
			context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
				new Formule
				{
					ActiviteId = zumbaCDH.ActiviteId,
					FormuleNom = "Abonnement annuel",
					IsActive = true,
					Tarif = 135
				});
			context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
				new Formule
				{
					ActiviteId = zumbaBEL.ActiviteId,
					FormuleNom = "Abonnement annuel",
					IsActive = true,
					Tarif = 135
				});
			context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
				new Formule
				{
					ActiviteId = zumbaBEL.ActiviteId,
					FormuleNom = "Abonnement semestre",
					IsActive = true,
					Tarif = 80
				});
			context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
				new Formule
				{
					ActiviteId = danseBEL1.ActiviteId,
					FormuleNom = "Abonnement annuel",
					IsActive = true,
					Tarif = 230
				});
			context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
				new Formule
				{
					ActiviteId = danseBEL2.ActiviteId,
					FormuleNom = "Abonnement annuel",
					IsActive = true,
					Tarif = 230
				});
			context.SaveChanges(); 
			#endregion

		}
	}
}