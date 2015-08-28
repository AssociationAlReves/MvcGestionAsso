using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcGestionAsso.Models
{
	public class ModeReglement
	{
		public int ModeReglementId { get; set; }

		public TypeReglement TypeReglement { get; set; }

		public int AdherentId { get; set; }
		public virtual Adherent Adherent { get; set; }

		public int FormuleId { get; set; }
		public virtual Formule Formule { get; set; }
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