using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Planification
	{
		[Range(1,7, ErrorMessage="Sélectionner un jour valide.")]
		public JourSemaine Jour { get; set; }
		public TimeSpan HeureDebut { get; set; }
		public TimeSpan HeureFin { get; set; }
	}

	public enum JourSemaine : int
	{
		Lundi = 1,
		Mardi = 2,
		Mercredi = 3,
		Jeudi = 4,
		Vendredi = 5,
		Samedi = 6,
		Dimanche = 7
	}
}