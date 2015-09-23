using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.BusinessRules
{
	public class BusinessRuleResult
	{
		 public bool Success { get; set; }
        public string Message { get; set; }


				public BusinessRuleResult()
        {
            Success = false;
        }
	}
}