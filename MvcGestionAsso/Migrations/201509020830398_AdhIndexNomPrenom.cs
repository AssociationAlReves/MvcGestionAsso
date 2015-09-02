namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdhIndexNomPrenom : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Adherents", "AK_Adherent_AdherentNom");
            CreateIndex("dbo.Adherents", new[] { "AdherentNom", "AdherentPrenom" }, name: "AK_Adherent_AdherentNom");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Adherents", "AK_Adherent_AdherentNom");
            CreateIndex("dbo.Adherents", "AdherentNom", name: "AK_Adherent_AdherentNom");
        }
    }
}
