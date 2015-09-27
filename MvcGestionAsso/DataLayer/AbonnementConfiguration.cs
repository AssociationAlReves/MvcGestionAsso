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
	public class AbonnementConfiguration : EntityTypeConfiguration<Abonnement>
	{
		public AbonnementConfiguration()
		{
			Property(a => a.AdherentId).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Abo_AdherentFormule", 1) { IsUnique = true }));

			Property(a => a.FormuleId).IsRequired()
				.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Abo_AdherentFormule", 2) { IsUnique = true }));

			Property(a => a.TypeReglement).IsRequired();

			Property(a => a.DateCreation);//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
			Property(a => a.DateModification);//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

			Ignore(a => a.LieuId);
			Ignore(a => a.ActiviteId);
		}
	}
}