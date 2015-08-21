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
	public class IntervenantConfiguration : EntityTypeConfiguration<Intervenant>
	{
		public IntervenantConfiguration ()
	{
		Property(i => i.IntervenantNom).HasMaxLength(50)
				.IsRequired()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Intervenant_IntervenantNom") { IsUnique = false}));
		Property(i => i.IntervenantPrenom).HasMaxLength(50).IsRequired();
		Property(i => i.NumeroSecuriteSociale).HasMaxLength(15).IsOptional();
		Property(i => i.DateCreation).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
		Property(i => i.DateModification).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
		
	}
	
	}
}