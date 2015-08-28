namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeReglementChgt : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ModeReglements", "AK_ModeReglement_ModeReglementCode");
            DropIndex("dbo.ModeReglements", "AK_ModeReglement_ModeReglementNom");
            AddColumn("dbo.ModeReglements", "TypeReglement", c => c.Int(nullable: false));
            DropColumn("dbo.ModeReglements", "ModeReglementCode");
            DropColumn("dbo.ModeReglements", "ModeReglementNom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModeReglements", "ModeReglementNom", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ModeReglements", "ModeReglementCode", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.ModeReglements", "TypeReglement");
            CreateIndex("dbo.ModeReglements", "ModeReglementNom", unique: true, name: "AK_ModeReglement_ModeReglementNom");
            CreateIndex("dbo.ModeReglements", "ModeReglementCode", unique: true, name: "AK_ModeReglement_ModeReglementCode");
        }
    }
}
