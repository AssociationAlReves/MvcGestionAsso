using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.DataLayer
{
	public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
	{
		public ApplicationUserConfiguration()
		{
			Property(au => au.FirstName).HasMaxLength(15).IsOptional();
			Property(au => au.LastName).HasMaxLength(15).IsOptional();
		}
	}
}