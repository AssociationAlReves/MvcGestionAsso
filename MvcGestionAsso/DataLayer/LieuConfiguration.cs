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
	public class LieuConfiguration : EntityTypeConfiguration<Lieu>
	{
		public LieuConfiguration()
		{
			Property(l => l.LieuCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Lieu_LieuCode") { IsUnique = true }));

			Property(l => l.LieuNom)
				.HasMaxLength(100)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Lieu_LieuNom") { IsUnique = true }));

			Property(l => l.CodePostal)
				.HasMaxLength(5)
				.IsOptional();

			Property(l => l.Ville)
				.HasMaxLength(150)
				.IsOptional();

			Property(l => l.Adresse)
				.HasMaxLength(150)
				.IsOptional();

			Property(l => l.Adresse2)
				.HasMaxLength(150)
				.IsOptional();
			
		}

	}
}