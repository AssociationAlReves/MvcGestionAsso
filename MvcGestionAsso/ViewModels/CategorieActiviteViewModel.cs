using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.ViewModels
{
	public class CategorieActiviteViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Catégorie parente")]
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "Vous devez saisir un nom de catégorie.")]
        [StringLength(50, ErrorMessage = "Le nom des catégories doivent comporter moins de 50 caractères.")]
        [Display(Name = "Catégorie")]
        public string CategorieActiviteNom { get; set; }
        public virtual List<Activite> Activites { get; set; }
    }
}