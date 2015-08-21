using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class ModeReglement
	{
		public int ModeReglementId { get; set; }

		public string ModeReglementCode { get; set; }

		public string ModeReglementNom { get; set; }

		public int AdherentId { get; set; }
		public virtual Adherent Adherent { get; set; }

		public int FormuleId { get; set; }
		public virtual Formule Formule { get; set; }
	}
}