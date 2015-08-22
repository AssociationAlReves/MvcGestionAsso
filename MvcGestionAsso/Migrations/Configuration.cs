namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcGestionAsso.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcGestionAsso.Models.ApplicationDbContext context)
        {
					MvcGestionAsso.Migrations.Seed.SampleDataSeed.Seed(context);
        }
    }
}
