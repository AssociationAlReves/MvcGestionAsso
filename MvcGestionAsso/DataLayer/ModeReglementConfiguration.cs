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
	public class ModeReglementConfiguration : EntityTypeConfiguration<ModeReglement>
	{
		public ModeReglementConfiguration()
		{
			Property( mr => mr.ModeReglementCode)
				.HasMaxLength(15)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_ModeReglement_ModeReglementCode") { IsUnique = true }));

			Property(mr => mr.ModeReglementNom)
				.HasMaxLength(100)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_ModeReglement_ModeReglementNom") { IsUnique = true }));


		}
	}
}