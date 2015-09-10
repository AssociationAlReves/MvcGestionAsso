using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.ViewModels
{
	public class ActiviteViewModel
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

		[Display(Name = "Planification")]
		public Planification Planification { get; set; }

		[Display(Name = "Durée (heures)")]
		public double DureeHeures
		{
			get { return (Planification.HeureFin - Planification.HeureDebut).TotalHours; }
		}

		public virtual Lieu Lieu { get; set; }
		[Display(Name = "Lieu")]
		public int LieuId { get; set; }

		public virtual CategorieActivite Categorie { get; set; }
		[Display(Name = "Catégorie")]
		public int CategorieActiviteId { get; set; }

		public virtual List<Mission> Missions { get; set; }

	}

}