using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using MvcGestionAsso.Models;

namespace MvcGestionAsso.DataLayer
{
	public class CategorieActiviteConfiguration : EntityTypeConfiguration<CategorieActivite>
	{
		public CategorieActiviteConfiguration()
		{
			Property(ca => ca.CategorieActiviteNom)
				.HasMaxLength(50)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_CategorieActivite_CategorieActiviteNom") { IsUnique = true }));

		}

	}
}