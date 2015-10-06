namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReglementAdherentNotAbo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reglements", new[] { "AbonnementId" });
            RenameColumn(table: "dbo.Reglements", name: "AbonnementId", newName: "Abonnement_AbonnementId");
            AddColumn("dbo.Reglements", "AdherentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reglements", "Abonnement_AbonnementId", c => c.Int());
            CreateIndex("dbo.Reglements", "AdherentId", name: "AK_Reglement_Adherent");
            CreateIndex("dbo.Reglements", "Abonnement_AbonnementId");
            AddForeignKey("dbo.Reglements", "AdherentId", "dbo.Adherents", "AdherentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reglements", "AdherentId", "dbo.Adherents");
            DropIndex("dbo.Reglements", new[] { "Abonnement_AbonnementId" });
            DropIndex("dbo.Reglements", "AK_Reglement_Adherent");
            AlterColumn("dbo.Reglements", "Abonnement_AbonnementId", c => c.Int(nullable: false));
            DropColumn("dbo.Reglements", "AdherentId");
            RenameColumn(table: "dbo.Reglements", name: "Abonnement_AbonnementId", newName: "AbonnementId");
            CreateIndex("dbo.Reglements", "AbonnementId");
        }
    }
}
