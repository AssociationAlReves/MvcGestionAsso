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
	public class ReglementConfiguration : EntityTypeConfiguration<Reglement>
	{
		public ReglementConfiguration()
		{
			Property(r => r.Montant).HasPrecision(18, 2).IsRequired();
			Property(r => r.MoyenPaiement).IsRequired();

			Property(r => r.IsAdhesionIncluse).IsRequired();

			Property(r => r.ChequeNumero).HasMaxLength(10).IsOptional()
				.HasColumnAnnotation("Index",
				new IndexAnnotation(new IndexAttribute("AK_Cheque_ChequeNumero") { IsUnique = false }
				));

			Property(r => r.ChequeBanque).HasMaxLength(80).IsOptional();
			Property(r => r.ChequeTitulaire).HasMaxLength(80).IsOptional();
			Property(r => r.ChequeDate).IsOptional();
			Property(r => r.ChequeDateEncaissement).IsOptional();

		}

	}
}