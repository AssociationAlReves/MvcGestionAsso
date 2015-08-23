namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserPropertieseN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 15));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 15));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "ZipCode", c => c.String(maxLength: 5));
            DropColumn("dbo.AspNetUsers", "Prenom");
            DropColumn("dbo.AspNetUsers", "Nom");
            DropColumn("dbo.AspNetUsers", "Adresse");
            DropColumn("dbo.AspNetUsers", "Ville");
            DropColumn("dbo.AspNetUsers", "CodePostal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CodePostal", c => c.String(maxLength: 5));
            AddColumn("dbo.AspNetUsers", "Ville", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "Adresse", c => c.String(maxLength: 30));
            AddColumn("dbo.AspNetUsers", "Nom", c => c.String(maxLength: 15));
            AddColumn("dbo.AspNetUsers", "Prenom", c => c.String(maxLength: 15));
            DropColumn("dbo.AspNetUsers", "ZipCode");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
