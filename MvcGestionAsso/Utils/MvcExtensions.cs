using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGestionAsso.Utils
{
	public static class MvcExtensions
	{
		public static void TraceModelErrors(this ModelStateDictionary modelState)
		{
			foreach (ModelState state in modelState.Values)
			{
				foreach (ModelError error in state.Errors)
				{
					Trace.TraceWarning("Model error: " + error.ErrorMessage);
				}
			}
		}
	}
}