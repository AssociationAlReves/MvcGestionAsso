namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegChequeIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reglements", "AK_Cheque_ChequeNumero");
            CreateIndex("dbo.Reglements", "ChequeNumero", name: "AK_Cheque_ChequeNumero");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reglements", "AK_Cheque_ChequeNumero");
            CreateIndex("dbo.Reglements", "ChequeNumero", unique: true, name: "AK_Cheque_ChequeNumero");
        }
    }
}
