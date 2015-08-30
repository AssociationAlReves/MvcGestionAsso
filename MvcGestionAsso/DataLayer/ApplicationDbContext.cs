using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.DataLayer
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public DbSet<Activite> Activites { get; set; }
		public DbSet<Adherent> Adherents { get; set; }
		public DbSet<CategorieActivite> CategoriesActivite { get; set; }
		public DbSet<Formule> Formules { get; set; }
		public DbSet<Intervenant> Intervenants { get; set; }
		public DbSet<Lieu> Lieux { get; set; }
		public DbSet<Mission> Missions { get; set; }
		public DbSet<Abonnement> Abonnements { get; set; }
		public DbSet<Reglement> Reglements { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Configurations.Add(new ActiviteConfiguration());
			modelBuilder.Configurations.Add(new AdherentConfiguration());
			modelBuilder.Configurations.Add(new CategorieActiviteConfiguration());
			modelBuilder.Configurations.Add(new FormuleConfiguration());
			modelBuilder.Configurations.Add(new IntervenantConfiguration());
			modelBuilder.Configurations.Add(new LieuConfiguration());
			modelBuilder.Configurations.Add(new MissionConfiguration());
			modelBuilder.Configurations.Add(new AbonnementConfiguration());
			modelBuilder.Configurations.Add(new ReglementConfiguration());
			modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public System.Data.Entity.DbSet<MvcGestionAsso.Models.ApplicationRole> IdentityRoles { get; set; }
	}
}