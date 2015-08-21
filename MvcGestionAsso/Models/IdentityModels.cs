using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcGestionAsso.DataLayer;

namespace MvcGestionAsso.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

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
				public DbSet<ModeReglement> ModesReglement { get; set; }
				public DbSet<Reglement> Reglements { get; set; }

				protected override void OnModelCreating(DbModelBuilder modelBuilder)
				{
					modelBuilder.Configurations.Add(new ActiviteConfiguration());
					modelBuilder.Configurations.Add(new AdherentConfiguration());
					modelBuilder.Configurations.Add(new CategorieActiviteConfiguration());
					modelBuilder.Configurations.Add(new FormuleConfiguration());
					modelBuilder.Configurations.Add(new IntervenantConfiguration());
					modelBuilder.Configurations.Add(new LieuConfiguration());
					modelBuilder.Configurations.Add(new MissionConfiguration());
					modelBuilder.Configurations.Add(new ModeReglementConfiguration());
					modelBuilder.Configurations.Add(new ReglementConfiguration());

					base.OnModelCreating(modelBuilder);
				}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}