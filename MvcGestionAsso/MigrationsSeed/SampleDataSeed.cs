using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;
using MvcGestionAsso.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LoremNET;
using System.Collections.Generic;

namespace MvcGestionAsso.Migrations.Seed
{
	public static class SampleDataSeed
	{
		public static void Seed(ApplicationDbContext context)
		{
			try
			{


				ApplicationUser adminUser;

				#region Admin user
				var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
				userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
				{
					AllowOnlyAlphanumericUserNames = true,
					RequireUniqueEmail = true
				};
				var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));

				string name = "boss@alreves.org";
				string password = "Pa$$w0rd";
				string firstname = "Admin";
				string lastName = "Association";
				string roleName = "Admin";

				var role = roleManager.FindByName(roleName);
				if (role == null)
				{
					role = new ApplicationRole(roleName);
					var roleResult = roleManager.Create(role);
				}

				adminUser = userManager.FindByName(name);
				if (adminUser == null)
				{
					adminUser = new ApplicationUser { UserName = name, Email = name, FirstName = firstname, LastName = lastName };
					var userResult = userManager.Create(adminUser, password);
					var result = userManager.SetLockoutEnabled(adminUser.Id, false);
				}

				var rolesForUser = userManager.GetRoles(adminUser.Id);

				if (!rolesForUser.Contains(role.Name))
				{
					var result = userManager.AddToRole(adminUser.Id, role.Name);
				}
				#endregion

				// --------------------------------------------------------------------
				// Data
				// ---

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
						Tarif = 135,
						DebutValidite = zumbaCDH.DateDebut,
						FinValidite = zumbaCDH.DateFin
					});
				context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
					new Formule
					{
						ActiviteId = zumbaBEL.ActiviteId,
						FormuleNom = "Abonnement annuel",
						IsActive = true,
						Tarif = 135,
						DebutValidite = zumbaBEL.DateDebut,
						FinValidite = zumbaBEL.DateFin
					});
				context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
					new Formule
					{
						ActiviteId = zumbaBEL.ActiviteId,
						FormuleNom = "Abonnement semestre",
						IsActive = true,
						Tarif = 80,
						DebutValidite = zumbaBEL.DateDebut,
						FinValidite = zumbaBEL.DateFin
					});
				context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
					new Formule
					{
						ActiviteId = danseBEL1.ActiviteId,
						FormuleNom = "Abonnement annuel",
						IsActive = true,
						Tarif = 230,
						DebutValidite = danseBEL1.DateDebut,
						FinValidite = danseBEL1.DateFin
					});
				context.Formules.AddOrUpdate(f => new { f.FormuleNom, f.ActiviteId },
					new Formule
					{
						ActiviteId = danseBEL2.ActiviteId,
						FormuleNom = "Abonnement annuel",
						IsActive = true,
						Tarif = 230,
						DebutValidite = danseBEL2.DateDebut,
						FinValidite = danseBEL2.DateFin
					});
				context.SaveChanges();
				#endregion
				Formule aboZumbaCDH = context.Formules.First(f => f.ActiviteId == zumbaCDH.ActiviteId);
				Formule abozumbaBEL = context.Formules.First(f => f.ActiviteId == zumbaBEL.ActiviteId && f.FormuleNom == "Abonnement annuel");
				Formule abozumbaBELSemestre = context.Formules.First(f => f.ActiviteId == zumbaBEL.ActiviteId && f.FormuleNom == "Abonnement semestre");
				Formule abodanseBEL1 = context.Formules.First(f => f.ActiviteId == danseBEL1.ActiviteId);
				Formule abodanseBEL2 = context.Formules.First(f => f.ActiviteId == danseBEL2.ActiviteId);
				Formule[] formules = context.Formules.ToArray();

				#region Adherents (1000 random)
				if (context.Adherents.Any() == false)
				{
					

					for (int i = 1; i <= 1000; i++)
					{

						context.Adherents.Add(new Adherent
							{
								AdherentNom = Lorem.Words(1).Limit(30),
								AdherentPrenom = Lorem.Words(1, Lorem.Chance(1, 20) ? 2 : 1).Limit(30),
								Adresse = (Lorem.Number(1, 1000) + " " + Lorem.Sentence(2, 5)).Limit(150),
								Adresse2 = (Lorem.Chance(20, 100) ? Lorem.Words(2, 5) : "").Limit(150),
								CertificatMedical = Lorem.Chance(80, 100),
								CodePostal = Lorem.Number(10000, 95999).ToString(),
								EditeurCourantId = adminUser.Id,
								EMail = Lorem.Email().Limit(80),
								Notes = Lorem.Chance(10, 100) ? Lorem.Paragraph(20, 2).Limit(150) : "",
								Statut = Lorem.RandomEnum<StatutAdherent>(),
								Telephone = "0" + Lorem.Number(100000000, 999999999).ToString(),
								Ville = Lorem.Words(1, 4).Limit(150),
								DateCreation = DateTime.Now,
								DateModification = DateTime.Now,
								DateResiliation = null
							});
						context.SaveChanges();

						#region FormuleAdherents
						Adherent adh = context.Adherents.Find(i);
						adh.Formules = new List<Formule>();

						adh.Formules.Add(Lorem.Random(formules));
						if (Lorem.Chance(1, 50)) // multiple formules
						{
							adh.Formules.Add(Lorem.Random(formules));
							adh.Formules = adh.Formules.Distinct(new FormuleComparer()).ToList(); // ensure it is distinct
						}
						context.SaveChanges();

						#region ModeReglement

						foreach (Formule f in adh.Formules)
						{
							context.ModesReglement.AddOrUpdate(m => new { m.AdherentId, m.FormuleId },
								new ModeReglement
								{
									TypeReglement = Lorem.RandomEnum<TypeReglement>(),
									AdherentId = adh.AdherentId,
									FormuleId = f.FormuleId,
								});
							context.SaveChanges();
							ModeReglement modeReg = context.ModesReglement.First(m => m.AdherentId == adh.AdherentId && m.FormuleId == f.FormuleId);
							#region Reglement
							if (Lorem.Chance(95, 100))
							{
								switch (modeReg.TypeReglement)
								{

									case TypeReglement.CarteBleue:
									case TypeReglement.Especes:
										context.Reglements.Add(new Reglement { AdherentId = adh.AdherentId, FormuleId = f.FormuleId, Montant = f.Tarif, IsAdhesionIncluse = true });
										break;
									case TypeReglement.Cheque_Comptant:

										AddReglementCheque(context, adh, f, 1);
										break;
									case TypeReglement.Cheque_2Fois:
										AddReglementCheque(context, adh, f, 2);
										break;
									case TypeReglement.Cheque_3Fois:
										AddReglementCheque(context, adh, f, 3);
										break;
									case TypeReglement.Cheque_4Fois:
										AddReglementCheque(context, adh, f, 4);
										break;
								}

								context.SaveChanges();
							}
							#endregion

						}
						#endregion



						#endregion


					}
				}
				#endregion

			}
			catch (System.Data.Entity.Validation.DbEntityValidationException e)
			{
				// Uncomment to debug seed
				if (System.Diagnostics.Debugger.IsAttached == false)
					System.Diagnostics.Debugger.Launch();

				// Check e.EntityValidationErrors

				throw;
			}
			catch(Exception e)
			{
				// Uncomment to debug seed
				if (System.Diagnostics.Debugger.IsAttached == false)
					System.Diagnostics.Debugger.Launch();
			}
		}

		private static void AddReglementCheque(ApplicationDbContext context, Adherent adh, Formule f, int numChq)
		{
			DateTime dtCheque;
			for (int n = 0; n < numChq; n++)
			{
				dtCheque = Lorem.DateTime(f.DebutValidite.Value, f.FinValidite.Value);
				context.Reglements.Add(new Reglement
				{
					AdherentId = adh.AdherentId,
					FormuleId = f.FormuleId,
					Montant = f.Tarif / numChq,
					IsAdhesionIncluse = n == 0,
					ChequeBanque = Lorem.Words(1, 4).Limit(80),
					ChequeTitulaire = Lorem.Chance(1, 30) ? Lorem.Words(2).Limit(80) : (adh.AdherentPrenom + " " + adh.AdherentNom).Trim().Limit(80),
					ChequeDate = dtCheque,
					ChequeDateEncaissement = Lorem.Chance(4, 10) ? null : (DateTime?)Lorem.DateTime(dtCheque, f.FinValidite.Value),
					ChequeNumero = (adh.AdherentId + 17 * f.FormuleId + 7 * n).ToString().PadLeft(10, '0'),
					MoyenPaiement = MoyenPaiement.Cheque
				});
			}
		}

		private class FormuleComparer : IEqualityComparer<Formule>
		{

			public bool Equals(Formule x, Formule y)
			{
				return x.FormuleId.Equals(y.FormuleId);
			}

			public int GetHashCode(Formule obj)
			{
				return obj.FormuleId.GetHashCode();
			}
		}
	}
}