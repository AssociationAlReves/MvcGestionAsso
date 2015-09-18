using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Formule
	{
		public int FormuleId { get; set; }

		[Display(Name="Nom de la formule")]
		[Required()]
		[StringLength(50)]
		public string FormuleNom { get; set; }

		[Display(Name = "Active")]
		public bool IsActive { get; set; }

		[Required()]
		[Range(1,1000)]
		[DisplayFormat(DataFormatString = "{0:C}",  ApplyFormatInEditMode = false)]
		public decimal Tarif { get; set; }

		[Display(Name="Activité")]
		public int ActiviteId { get; set; }
		public virtual Activite Activite { get; set; }

		public virtual List<Abonnement> Abonnements { get; set; }

		//public virtual List<Reglement> Reglements { get; set; }
	}
}