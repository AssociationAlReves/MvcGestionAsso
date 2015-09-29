using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Reglement
	{
		public int ReglementId { get; set; }

		public decimal Montant { get; set; }

		[Display(Name = "Moyen de paiement")]
		public MoyenPaiement MoyenPaiement { get; set; }

		[Display(Name = "Inclut l'adhésion")]
		public bool IsAdhesionIncluse { get; set; }

		[Display(Name = "Numéro de chèque")]
		public string ChequeNumero { get; set; }
		[Display(Name = "Etablissement bancaire")]
		public string ChequeBanque { get; set; }
		[Display(Name = "Nom du titulaire")]
		public string ChequeTitulaire { get; set; }
		[Display(Name = "Date du chèque")]
		public DateTime? ChequeDate { get; set; }
		[Display(Name = "Date d'encaissement")]
		public DateTime? ChequeDateEncaissement { get; set; }

		//public int AdherentId { get; set; }
		//public virtual Adherent Adherent { get; set; }

		//public int FormuleId { get; set; }
		//public virtual Formule Formule { get; set; }

		[Display(Name = "Abonnement")]
		public int AbonnementId { get; set; }
		[Display(Name = "Abonnement")]
		public virtual Abonnement Abonnement { get; set; }
	}

	public enum MoyenPaiement
	{
		[Display(Name="Espèces")]
		Especes = 10,
		[Display(Name = "Chèque")]
		Cheque = 20,
		//CB = 30
	}

}