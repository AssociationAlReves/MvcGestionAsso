namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategorieActivites", "ParentId", c => c.Int());
            CreateIndex("dbo.CategorieActivites", "ParentId");
            AddForeignKey("dbo.CategorieActivites", "ParentId", "dbo.CategorieActivites", "CategorieActiviteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategorieActivites", "ParentId", "dbo.CategorieActivites");
            DropIndex("dbo.CategorieActivites", new[] { "ParentId" });
            DropColumn("dbo.CategorieActivites", "ParentId");
        }
    }
}
