using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Reglement
	{
		public int ReglementId { get; set; }

		public decimal Montant { get; set; }
		public MoyenPaiement MoyenPaiement { get; set; }

		public bool IsAdhesionIncluse { get; set; }

		public string ChequeNumero { get; set; }
		public string ChequeBanque { get; set; }
		public string ChequeTitulaire { get; set; }
		public DateTime? ChequeDate { get; set; }
		public DateTime? ChequeDateEncaissement { get; set; }

		public int AdherentId { get; set; }
		public virtual Adherent Adherent { get; set; }

		public int FormuleId { get; set; }
		public virtual Formule Formule { get; set; }
	}

	public enum MoyenPaiement
	{
		Especes = 10,
		Cheque = 20,
		CB = 30
	}

}