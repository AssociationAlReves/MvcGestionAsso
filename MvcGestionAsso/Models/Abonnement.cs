using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Abonnement
	{
		public int AbonnementId { get; set; }

		public int AdherentId { get; set; }
		public Adherent Adherent { get; set; }

		public virtual List<Reglement> Reglements { get; set; }

		public int FormuleId { get; set; }
		public Formule Formule { get; set; }

		public TypeReglement TypeReglement { get; set; }

		public DateTime DateDebut { get; set; }

		public DateTime DateFin { get; set; }
	}

	public enum TypeReglement
	{
		Especes = 10,
		Cheque_Comptant = 15,
		Cheque_2Fois = 20,
		Cheque_3Fois = 30,
		Cheque_4Fois = 40,
		CarteBleue = 50
	}
}