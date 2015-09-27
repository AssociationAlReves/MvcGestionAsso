using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcGestionAsso.Utils
{
	public static class MvcExtensions
	{
		public static void TraceModelErrors(this ModelStateDictionary modelState)
		{

			foreach (string error in GetModelErrors(modelState))
			{
				Trace.TraceWarning("Model error: " + error);
			}

		}

		public static List<string> GetModelErrors(this ModelStateDictionary modelState)
		{
			return modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
		}
	}
}