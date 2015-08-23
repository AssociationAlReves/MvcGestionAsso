﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;
using SendGrid;

namespace MvcGestionAsso
{
	public class EmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			const string userName = "MvcGestionAsso Admin";
			const string from = "admin@mvcgestionasso.org";

			// Create the email object first, then add the properties.
			SendGridMessage myMessage = new SendGridMessage();
			myMessage.AddTo(message.Destination);
			myMessage.From = new MailAddress(from, userName);
			myMessage.Subject = message.Subject;
			myMessage.Text = message.Subject;

			// Create credentials, specifying your user name and password.
			var credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendGrid_UserName"], ConfigurationManager.AppSettings["SendGrid_Password"]);

			// Create an Web transport for sending email, using credentials...
			var transportWeb = new Web(credentials);

			// Send the email.
			return transportWeb.DeliverAsync(myMessage);
		}
	}

	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Connectez votre service SMS ici pour envoyer un message texte.
			return Task.FromResult(0);
		}
	}

	// Configurer l'application que le gestionnaire des utilisateurs a utilisée dans cette application. UserManager est défini dans ASP.NET Identity et est utilisé par l'application.
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
			: base(store)
		{
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
			// Configurer la logique de validation pour les noms d'utilisateur
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configurer la logique de validation pour les mots de passe
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			// Configurer les valeurs par défaut du verrouillage de l'utilisateur
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Inscrire les fournisseurs d'authentification à 2 facteurs. Cette application utilise le téléphone et les e-mails comme procédure de réception de code pour confirmer l'utilisateur
			// Vous pouvez écrire votre propre fournisseur et le connecter ici.
			manager.RegisterTwoFactorProvider("Code téléphonique ", new PhoneNumberTokenProvider<ApplicationUser>
			{
				MessageFormat = "Votre code de sécurité est {0}"
			});
			manager.RegisterTwoFactorProvider("Code d'e-mail", new EmailTokenProvider<ApplicationUser>
			{
				Subject = "Code de sécurité",
				BodyFormat = "Votre code de sécurité est {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
						new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}

	// Configurer le gestionnaire de connexion d'application qui est utilisé dans cette application.
	public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
	{
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
		{
			return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
		}

		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
		{
			return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
		}
	}

	// Configurer le gestionnaire de rôles. 
	public class ApplicationRoleManager : RoleManager<ApplicationRole>
	{
		public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
			: base(roleStore)
		{

		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
		{
			return new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
		}
	}
}