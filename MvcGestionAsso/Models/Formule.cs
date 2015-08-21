using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Formule
	{
		public int FormuleId { get; set; }

		public string FormuleNom { get; set; }

		public DateTime DebutValidite { get; set; }
		public DateTime FinValidite { get; set; }

		public bool IsActive { get; set; }

		public decimal Tarif { get; set; }

		public int ActiviteId { get; set; }
		public virtual Activite Activite { get; set; }

		public virtual List<Adherent> AdherentsAbonnes { get; set; }

		//public virtual List<Reglement> Reglements { get; set; }
	}
}