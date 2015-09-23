namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiviteLieuIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Activites", "AK_Activite_ActiviteNomEtLieu");
            CreateIndex("dbo.Activites", "LieuId", name: "AK_Activite_ActiviteLieu");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Activites", "AK_Activite_ActiviteLieu");
            CreateIndex("dbo.Activites", new[] { "ActiviteNom", "LieuId" }, unique: true, name: "AK_Activite_ActiviteNomEtLieu");
        }
    }
}
