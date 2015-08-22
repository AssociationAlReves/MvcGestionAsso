namespace MvcGestionAsso.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using MvcGestionAsso.DataLayer;

	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			MvcGestionAsso.Migrations.Seed.SampleDataSeed.Seed(context);
		}
	}
}
