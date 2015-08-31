using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Activite
	{
		public int ActiviteId { get; set; }

		[Required(ErrorMessage = "Le nom de l'activité est requis.")]
		[StringLength(100, ErrorMessage = "Le nom de l'activité doit comporter moins de 100 caractères.")]
		[Display(Name = "Nom de l'activité")]
		public string ActiviteNom { get; set; }

		[Required(ErrorMessage = "Le code de l'activité est requis.")]
		[StringLength(15, ErrorMessage = "Le code de l'activité doit comporter moins de 15 caractères.")]
		[Display(Name = "Code de l'activité")]
		public string ActiviteCode { get; set; }

		[Display(Name = "Date de début")]
		public DateTime DateDebut { get; set; }
		[Display(Name = "Date de fin")]
		public DateTime DateFin { get; set; }

		[Display(Name = "Durée (heures)")]
		[Range(0.5, 8, ErrorMessage = "La durée doit être comprise entre 0.5 et 8 heures")]
		public float DureeHeures { get; set; }

		public virtual Lieu Lieu { get; set; }
		public int LieuId { get; set; }

		public CategorieActivite Categorie { get; set; }
		[Display(Name = "Catégorie")]
		public int CategorieActiviteId { get; set; }

		public virtual List<Mission> Missions { get; set; }

	}

}