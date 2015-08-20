using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Lieu
	{
		public int LieuId { get; set; }

		public string LieuCode { get; set; }
		public string LieuNom { get; set; }

		public string Adresse { get; set; }
		public string Adresse2 { get; set; }

		public string CodePostal { get; set; }

		public string Ville { get; set; }
	}
}