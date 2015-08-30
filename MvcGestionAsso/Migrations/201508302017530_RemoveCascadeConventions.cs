namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCascadeConventions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Abonnements", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.Abonnements", "FormuleId", "dbo.Formules");
            DropForeignKey("dbo.Reglements", "AbonnementId", "dbo.Abonnements");
            DropForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites");
            DropForeignKey("dbo.Activites", "LieuId", "dbo.Lieux");
            DropForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            AddForeignKey("dbo.Abonnements", "AdherentId", "dbo.Adherents", "AdherentId");
            AddForeignKey("dbo.Abonnements", "FormuleId", "dbo.Formules", "FormuleId");
            AddForeignKey("dbo.Reglements", "AbonnementId", "dbo.Abonnements", "AbonnementId");
            AddForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites", "ActiviteId");
            AddForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites", "CategorieActiviteId");
            AddForeignKey("dbo.Activites", "LieuId", "dbo.Lieux", "LieuId");
            AddForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites", "ActiviteId");
            AddForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants", "IntervenantId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants");
            DropForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Activites", "LieuId", "dbo.Lieux");
            DropForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites");
            DropForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reglements", "AbonnementId", "dbo.Abonnements");
            DropForeignKey("dbo.Abonnements", "FormuleId", "dbo.Formules");
            DropForeignKey("dbo.Abonnements", "AdherentId", "dbo.Adherents");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants", "IntervenantId", cascadeDelete: true);
            AddForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites", "ActiviteId", cascadeDelete: true);
            AddForeignKey("dbo.Activites", "LieuId", "dbo.Lieux", "LieuId", cascadeDelete: true);
            AddForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites", "CategorieActiviteId", cascadeDelete: true);
            AddForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites", "ActiviteId", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Adherents", "EditeurCourantId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reglements", "AbonnementId", "dbo.Abonnements", "AbonnementId", cascadeDelete: true);
            AddForeignKey("dbo.Abonnements", "FormuleId", "dbo.Formules", "FormuleId", cascadeDelete: true);
            AddForeignKey("dbo.Abonnements", "AdherentId", "dbo.Adherents", "AdherentId", cascadeDelete: true);
        }
    }
}
