namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiviteHeureDebutFin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activites", "HeureDebut", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Activites", "HeureFin", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Activites", "DureeHeures");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activites", "DureeHeures", c => c.Single(nullable: false));
            DropColumn("dbo.Activites", "HeureFin");
            DropColumn("dbo.Activites", "HeureDebut");
        }
    }
}
