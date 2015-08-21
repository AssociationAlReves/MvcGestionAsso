using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Intervenant
	{
		public int IntervenantId { get; set; }

		public string IntervenantNom { get; set; }
		public string IntervenantPrenom { get; set; }

		public string NumeroSecuriteSociale { get; set; }

		public DateTime DateCreation { get; set; }
		public DateTime DateModification { get; set; }

		public virtual List<Mission> Missions { get; set; }
	}
}