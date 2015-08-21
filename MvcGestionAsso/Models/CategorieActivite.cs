using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcGestionAsso.Models
{
	public class CategorieActivite
	{
		public int CategorieActiviteId { get; set; }

		public string CategorieActiviteNom { get; set; }

		public virtual List<Activite> Activites { get; set; }
	}
}
