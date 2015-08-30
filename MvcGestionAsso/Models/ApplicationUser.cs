using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
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

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }

		public string FullName
		{
			get { return FirstName + " " + LastName; }
		}

		public string AddressBlock
		{
			get
			{
				string addressBlock = string.Format("{0}<br/>{1} {2}", Address, ZipCode, City).Trim();
				return addressBlock == "<br/>" ? string.Empty : addressBlock;
			}
		}

		public IEnumerable<SelectListItem> RolesList { get; set; }
	}
}