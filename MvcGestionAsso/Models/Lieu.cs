using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public class Lieu
	{
		public int LieuId { get; set; }

		[Required(ErrorMessage = "Le nom du lieu est requis.")]
		[StringLength(100, ErrorMessage = "Le nom du lieu doit comporter moins de 100 caractères.")]
		[Display(Name = "Lieu")]
		public string LieuNom { get; set; }

		[Required(ErrorMessage = "Le code du lieu est requis.")]
		[StringLength(15, ErrorMessage = "Le code du lieu doit comporter moins de 15 caractères.")]
		[Display(Name = "Code")]
		public string LieuCode { get; set; }

		[StringLength(150, ErrorMessage = "L'adresse du lieu doit comporter moins de 150 caractères.")]
		public string Adresse { get; set; }

		[StringLength(150, ErrorMessage = "Le complément d'adresse du lieu doit comporter moins de 150 caractères.")]
		[Display(Name = "Complément d'adresse")]
		public string Adresse2 { get; set; }

		[StringLength(5, MinimumLength = 5, ErrorMessage = "Le code postal doit comporter 5 caractères.")]
		[Display(Name = "Code postal")]
		public string CodePostal { get; set; }

		[StringLength(150, ErrorMessage = "La ville du lieu doit comporter moins de 150 caractères.")]
		public string Ville { get; set; }

		public virtual List<Activite> Activites { get; set; }
	}
}