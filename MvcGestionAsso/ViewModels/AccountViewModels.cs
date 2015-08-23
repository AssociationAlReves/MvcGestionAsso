using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcGestionAsso.Models
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required]
		[Display(Name = "Courrier électronique")]
		public string Email { get; set; }
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl { get; set; }
	}

	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}

	public class VerifyCodeViewModel
	{
		[Required]
		public string Provider { get; set; }

		[Required]
		[Display(Name = "Code")]
		public string Code { get; set; }
		public string ReturnUrl { get; set; }

		[Display(Name = "Mémoriser ce navigateur ?")]
		public bool RememberBrowser { get; set; }

		public bool RememberMe { get; set; }
	}

	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Courrier électronique")]
		public string Email { get; set; }
	}

	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Courrier électronique")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; }

		[Display(Name = "Mémoriser le mot de passe ?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Courrier électronique")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirmer le mot de passe ")]
		[Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Prénom")]
		[StringLength(15, ErrorMessage = "Le prénom doit contenir moins de 15 caratères.")]
		public string FirstName { get; set; }
		[StringLength(15, ErrorMessage = "Le nom doit contenir moins de 15 caratères.")]
		public string LastName { get; set; }
		[StringLength(30, ErrorMessage = "L'adresse doit contenir moins de 30 caratères.")]
		public string Address { get; set; }
		[StringLength(20, ErrorMessage = "La ville doit contenir moins de 30 caratères.")]
		public string City { get; set; }
		[Display(Name = "Code postal")]
		[StringLength(5, MinimumLength = 5, ErrorMessage = "Le code postal doit contenir 5 caratères.")]
		public string ZipCode { get; set; }
	}

	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Courrier électronique")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Mot de passe")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirmer le mot de passe")]
		[Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "E-mail")]
		public string Email { get; set; }
	}
}
