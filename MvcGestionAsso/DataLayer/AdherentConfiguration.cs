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
	public class AdherentConfiguration : EntityTypeConfiguration<Adherent>
	{
		public AdherentConfiguration()
		{
			Property(a => a.AdherentNom)
				.IsRequired()
				.HasMaxLength(30)
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Adherent_AdherentNom") { IsUnique = false }));

			Property(a => a.AdherentPrenom).HasMaxLength(30).IsRequired();
			Property(a => a.Famille).HasMaxLength(30).IsOptional();
			Property(a => a.Notes).HasMaxLength(150).IsOptional();
			Property(a => a.EMail).HasMaxLength(80).IsOptional();
			Property(a => a.Telephone).HasMaxLength(10).IsOptional();
			Property(a => a.Adresse).HasMaxLength(150).IsOptional();
			Property(a => a.Adresse2).HasMaxLength(150).IsOptional();
			Property(a => a.CodePostal).HasMaxLength(5).IsOptional();
			Property(a => a.Ville).HasMaxLength(150).IsOptional();

			Property(a => a.DateCreation).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
			Property(a => a.DateCreation).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
			Property(a => a.DateResiliation).IsOptional();

			Property(a => a.Statut).IsRequired();

			Property(a => a.CertificatMedical).IsOptional();


		}
	}
}