namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormuleNoDates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Formules", "DebutValidite");
            DropColumn("dbo.Formules", "FinValidite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Formules", "FinValidite", c => c.DateTime());
            AddColumn("dbo.Formules", "DebutValidite", c => c.DateTime());
        }
    }
}
