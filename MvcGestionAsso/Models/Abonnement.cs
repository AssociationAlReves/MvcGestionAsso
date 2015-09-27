using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Abonnement : ITrackable
	{
		public int AbonnementId { get; set; }

		[Display(Name = "Adhérent")]
		public int AdherentId { get; set; }
		public Adherent Adherent { get; set; }

		public virtual List<Reglement> Reglements { get; set; }

		[Required(ErrorMessage = "Vous devez sélectionner une formule.")]
		[Range(1, int.MaxValue, ErrorMessage = "Vous devez sélectionner une formule.")]
		[Display(Name = "Formule")]
		public int FormuleId { get; set; }
		public Formule Formule { get; set; }

		[Required]
		[Display(Name = "Type de règlement")]
		[Range(1, 100, ErrorMessage = "Le type de règlement est requis.")]
		public TypeReglement TypeReglement { get; set; }

		[Display(Name = "Créé le")]
		public DateTime? DateCreation { get; set; }
		[Display(Name = "Modifié le")]
		public DateTime? DateModification { get; set; }

		[Required(ErrorMessage = "Vous devez sélectionner un lieu.")]
		[Range(1, int.MaxValue, ErrorMessage = "Vous devez sélectionner un lieu.")]
		[Display(Name = "Lieu")]
		public int LieuId { get; set; }

		[Required(ErrorMessage = "Vous devez sélectionner une activité.")]
		[Range(1, int.MaxValue, ErrorMessage = "Vous devez sélectionner une activité.")]
		[Display(Name = "Activité")]
		public int ActiviteId { get; set; }

		public Lieu Lieu
		{
			get { return Formule.Activite.Lieu; }
		}

		public Activite Activite
		{
			get { return Formule.Activite; }
		}
	}

	public enum TypeReglement
	{
		[Display(Name = "Espèces")]
		Especes = 10,
		[Display(Name = "Chèque comptant")]
		Cheque_Comptant = 15,
		//[Display(Name = "Chèque 2x sans frais")]
		//Cheque_2Fois = 20,
		[Display(Name = "Chèque 3x sans frais")]
		Cheque_3Fois = 30,
		//[Display(Name = "Carte bleue")]
		//CarteBleue = 50
	}
}