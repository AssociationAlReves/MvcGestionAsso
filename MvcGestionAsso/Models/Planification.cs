using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Planification
	{
		public JourSemaine Jour { get; set; }
		public TimeSpan HeureDebut { get; set; }
		public TimeSpan HeureFin { get; set; }
	}

	public enum JourSemaine : int
	{
		NonDefini = 0,
		Lundi = 1,
		Mardi = 2,
		Mercredi = 3,
		Jeudi = 4,
		Vendredi = 5,
		Samedi = 6,
		Dimanche = 7
	}
}