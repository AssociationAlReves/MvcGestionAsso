using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.BusinessRules
{
	public static class AssoBusinessRules
	{
		public static BusinessRuleResult CanDelete(ApplicationDbContext context, Activite activite)
		{
			if (activite == null)
				return new BusinessRuleResult { Success = false, Message = "L'activité n'existe pas." };

			bool hasFormules = context.Formules.Where(f => f.ActiviteId == activite.ActiviteId)
																					.Any();

			if (hasFormules)
				return new BusinessRuleResult() { Success = false, Message = "L'activté ne peut être supprimée car des formules y sont liées." };
			else
				return new BusinessRuleResult() { Success = true };
		}

		public static BusinessRuleResult CanDelete(ApplicationDbContext context, Abonnement abonnement)
		{
			if (abonnement == null)
				return new BusinessRuleResult { Success = false, Message = "L'abonnement n'existe pas." };

			// TODO check if reglements in same period than the abonnement => not deletable

			/*
			bool hasReglements = context.Reglements.Where(r => r.AbonnementId == abonnement.AbonnementId)
																					.Any();
			if (hasReglements)
				return new BusinessRuleResult() { Success = false, Message = "L'abonnement ne peut être supprimée car des règlements y sont liées." };
			else
				return new BusinessRuleResult() { Success = true };
			 */

			return new BusinessRuleResult() { Success = true };
		}

	}
}