namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Prenom", c => c.String(maxLength: 15));
            AddColumn("dbo.AspNetUsers", "Nom", c => c.String(maxLength: 15));
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "Ville", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "CodePostal", c => c.String(maxLength: 5));
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.AspNetUsers", "CodePostal");
            DropColumn("dbo.AspNetUsers", "Ville");
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Nom");
            DropColumn("dbo.AspNetUsers", "Prenom");
        }
    }
}
