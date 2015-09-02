namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdhNoEditeur : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers");
            DropIndex("dbo.Adherents", new[] { "EditeurCourantId" });
            DropColumn("dbo.Adherents", "EditeurCourantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adherents", "EditeurCourantId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Adherents", "EditeurCourantId");
            AddForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers", "Id");
        }
    }
}
