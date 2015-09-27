namespace MvcGestionAsso.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<MvcGestionAsso.DataLayer.ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(MvcGestionAsso.DataLayer.ApplicationDbContext context)
		{
			MvcGestionAsso.Migrations.Seed.SampleDataSeed.Seed(context);
		}
	}
}
