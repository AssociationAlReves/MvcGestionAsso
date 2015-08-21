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
		public DateTime DateFin { get; set; }

		public float DureeHeures { get; set; }

		public virtual Lieu Lieu { get; set; }
		public int LieuId { get; set; }

		public CategorieActivite Categorie { get; set; }
		public int CategorieActiviteId { get; set; }

		public virtual List<Mission> Missions { get; set; }

	}

}