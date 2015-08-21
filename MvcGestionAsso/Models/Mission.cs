using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcGestionAsso.Models
{
	public class Mission
	{
		public int MissionId { get; set; }

		public string Description { get; set; }
		public string Notes { get; set; }

		public decimal SalaireHoraire { get; set; }

		public DateTime DateDebut { get; set; }
		public DateTime DateFin { get; set; }

		public int IntervenantId { get; set; }
		public virtual Intervenant Intervenant { get; set; }

		public int ActiviteId { get; set; }
		public virtual Activite Activite { get; set; }

	}
}
