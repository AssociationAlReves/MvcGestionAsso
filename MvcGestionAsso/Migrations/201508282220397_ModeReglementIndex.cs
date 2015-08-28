namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeReglementIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ModeReglements", new[] { "AdherentId" });
            DropIndex("dbo.ModeReglements", new[] { "FormuleId" });
            CreateIndex("dbo.ModeReglements", new[] { "FormuleId", "AdherentId" }, unique: true, name: "AK_ModeReglement_Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ModeReglements", "AK_ModeReglement_Index");
            CreateIndex("dbo.ModeReglements", "FormuleId");
            CreateIndex("dbo.ModeReglements", "AdherentId");
        }
    }
}
