using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Activite
	{
		public int ActiviteId { get; set; }

		public string ActiviteNom { get; set; }

		public string ActiviteCode { get; set; }

		public DateTime DateDebut { get; set; }
		public DateTime DateFin{ get; set; }
		
		public Lieu Lieu { get; set; }

	}
}