using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Utils
{
	public static class StringExtensions
	{
		public static string Limit(this string input, int length)
		{
			if (input == null) return null;
			return input.Substring(0, Math.Min(input.Length, length));
		}
	}
}