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
			Property(au => au.Address).HasMaxLength(30).IsOptional();
			Property(au => au.City).HasMaxLength(30).IsOptional();
			Property(au => au.ZipCode).HasMaxLength(5).IsOptional();
			Ignore(au => au.RolesList);
		}
	}
}