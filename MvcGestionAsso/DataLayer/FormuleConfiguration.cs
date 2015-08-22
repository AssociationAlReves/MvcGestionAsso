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
	public class FormuleConfiguration : EntityTypeConfiguration<Formule>
	{
		public FormuleConfiguration()
		{
			Property(f => f.FormuleNom)
				.HasMaxLength(50)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Formule_FormuleNom") { IsUnique = false }));

			Property(f => f.DebutValidite)
				.IsOptional();

			Property(f => f.FinValidite)
				.IsOptional();

			Property(f => f.IsActive)
				.IsOptional();

			Property(f => f.Tarif)
				.IsRequired()
				.HasPrecision(18, 2);
		}
	}
}