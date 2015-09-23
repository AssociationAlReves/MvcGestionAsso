using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcGestionAsso.DataLayer;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.BusinessRules
{
	public static class ActiviteBR
	{
		public static BusinessRuleResult CanDelete(ApplicationDbContext context, Activite activite)
		{
			bool hasFormules = context.Formules.Where(f => f.ActiviteId == activite.ActiviteId)
																					.Any();

			if (hasFormules)
				return new BusinessRuleResult() { Success = false, Message = "L'activté ne peut être supprimée car des formules y sont liées." };
			else
				return new BusinessRuleResult() { Success = true };
		}

	}
}