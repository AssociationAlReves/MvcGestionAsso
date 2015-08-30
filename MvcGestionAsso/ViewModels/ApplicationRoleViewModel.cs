using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.ViewModels
{
	public class ApplicationRoleViewModel
	{
		public string Id { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez fournir un nom pour le rôle.")]
		[StringLength(256, ErrorMessage= "Le nom du rôle doit comporter moins de 256 caractères.")]
		[Display(Name = "Nom du rôle")]
		public string Name { get; set; }
	}
}