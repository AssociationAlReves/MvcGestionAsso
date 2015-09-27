using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGestionAsso.Models
{
	public interface ITrackable
	{
		DateTime? DateCreation { get; set; }

		DateTime? DateModification { get; set; }
	}
}