namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdherentDateModif : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Adherents", "DateModification", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Adherents", "DateModification", c => c.DateTime(nullable: false));
        }
    }
}
