namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdherentAssurance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adherents", "AttestationAssurance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adherents", "AttestationAssurance");
        }
    }
}
