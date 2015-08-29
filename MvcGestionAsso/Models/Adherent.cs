using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Adherent
	{
		public int AdherentId { get; set; }

		public string AdherentNom { get; set; }

		public string AdherentPrenom { get; set; }

		public string Famille { get; set; }

		public string Notes { get; set; }

		public string EMail { get; set; }

		public string Telephone { get; set; }

		public string Adresse { get; set; }

		public string Adresse2 { get; set; }

		public string CodePostal { get; set; }

		public string Ville { get; set; }

		public DateTime DateCreation { get; set; }
		public DateTime DateModification { get; set; }

		public DateTime? DateResiliation { get; set; }

		public StatutAdherent Statut { get; set; }

		public bool CertificatMedical { get; set; }

		public virtual List<Abonnement> Abonnements { get; set; }

		public ApplicationUser EditeurCourant { get; set; }
		public string EditeurCourantId { get; set; }
		//public virtual List<ModeReglement> ModeReglement { get; set; }
		
	}

	// Diagramme d'états inspiré de http://gerer-mon-association.fr/images/stories/Diagramme_etats_adherents.png
	public enum StatutAdherent
	{
		Cree = 0,
		Valide = 10,
		AdhesionAJour = 20,
		Resilie = 30,
		Annule = -10,
		Supprime = -20
	}


}