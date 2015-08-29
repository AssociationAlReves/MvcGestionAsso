namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AboAdherent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reglements", "Adherent_AdherentId", "dbo.Adherents");
            DropIndex("dbo.Reglements", new[] { "Adherent_AdherentId" });
            DropColumn("dbo.Reglements", "Adherent_AdherentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reglements", "Adherent_AdherentId", c => c.Int());
            CreateIndex("dbo.Reglements", "Adherent_AdherentId");
            AddForeignKey("dbo.Reglements", "Adherent_AdherentId", "dbo.Adherents", "AdherentId");
        }
    }
}
