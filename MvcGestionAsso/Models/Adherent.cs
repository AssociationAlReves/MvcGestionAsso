using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Adherent
	{
		public int AdherentId { get; set; }

		[Required()]
		[StringLength(30)]
		[Display(Name = "Nom")]
		public string AdherentNom { get; set; }

		[Required()]
		[StringLength(30)]
		[Display(Name = "Prénom")]
		public string AdherentPrenom { get; set; }

		[StringLength(30)]
		public string Famille { get; set; }

		[StringLength(150)]
		public string Notes { get; set; }

		[StringLength(80)]
		[EmailAddress()]
		public string EMail { get; set; }

		[StringLength(10)]
		[RegularExpression("^0[1-9][0-9]{8}$", ErrorMessage = "Ce numéro de téléphone n'est pas valide.")]
		[Display(Name = "Téléphone")]
		public string Telephone { get; set; }

		[StringLength(150)]
		public string Adresse { get; set; }

		[StringLength(150)]
		[Display(Name = "Complément d'adresse")]
		public string Adresse2 { get; set; }

		[Display(Name = "Code postal")]
		[RegularExpression("^(2[ab]|0[1-9]|[1-9][0-9])[0-9]{3}$", ErrorMessage = "Le code postal est invalide.")]
		public string CodePostal { get; set; }

		[StringLength(150)]
		public string Ville { get; set; }


		public DateTime DateCreation { get; set; }
		public DateTime DateModification { get; set; }

		public DateTime? DateResiliation { get; set; }

		public StatutAdherent Statut { get; set; }

		[Display(Name = "Certif. médical")]
		public bool CertificatMedical { get; set; }

		public virtual List<Abonnement> Abonnements { get; set; }

		//public ApplicationUser EditeurCourant { get; set; }
		//public string EditeurCourantId { get; set; }
		//public virtual List<ModeReglement> ModeReglement { get; set; }

	}

	// Diagramme d'états inspiré de http://gerer-mon-association.fr/images/stories/Diagramme_etats_adherents.png
	public enum StatutAdherent
	{
		[Display(Name = "Créé")]
		Cree = 0,
		[Display(Name = "Valide")]
		Valide = 10,
		[Display(Name = "Cotisation à jour")]
		AdhesionAJour = 20,
		[Display(Name = "Résilié")]
		Resilie = 30,
		[Display(Name = "Annulé")]
		Annule = -10,
		[Display(Name = "Supprimé")]
		Supprime = -20
	}


}