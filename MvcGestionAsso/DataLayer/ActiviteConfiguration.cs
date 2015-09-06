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
	public class ActiviteConfiguration : EntityTypeConfiguration<Activite>
	{
		public ActiviteConfiguration()
		{
			Property(a => a.ActiviteCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Activite_ActiviteCode") { IsUnique = true }));

			Property(a => a.ActiviteNom)
				.HasMaxLength(100)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Activite_ActiviteNom") { IsUnique = true }));

			Property(a => a.DateDebut)
				.IsRequired();

			Property(a => a.DateFin)
				.IsRequired();

			Property(a => a.HeureDebut)
				.IsRequired();

			Property(a => a.HeureFin)
				.IsRequired();

		}

	}
}