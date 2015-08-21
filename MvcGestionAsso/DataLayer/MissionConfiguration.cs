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
	public class MissionConfiguration : EntityTypeConfiguration<Mission>
	{

		public MissionConfiguration()
		{
			Property(m => m.SalaireHoraire).HasPrecision(18, 2).IsRequired();

			Property(m => m.Description).HasMaxLength(80).IsOptional();
			Property(m => m.Notes).HasMaxLength(200).IsOptional();
			
			Property(m => m.DateDebut).IsRequired();
			Property(m => m.DateFin).IsRequired();
			
		}
	}
}