namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivitePlanif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activites", "PlanifJour", c => c.Int(nullable: false));
            AddColumn("dbo.Activites", "PlanfHeureDebut", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Activites", "PlanfHeureFin", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Activites", "HeureDebut");
            DropColumn("dbo.Activites", "HeureFin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activites", "HeureFin", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Activites", "HeureDebut", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Activites", "PlanfHeureFin");
            DropColumn("dbo.Activites", "PlanfHeureDebut");
            DropColumn("dbo.Activites", "PlanifJour");
        }
    }
}
