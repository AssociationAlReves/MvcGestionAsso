namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abonnements",
                c => new
                    {
                        AbonnementId = c.Int(nullable: false, identity: true),
                        AdherentId = c.Int(nullable: false),
                        FormuleId = c.Int(nullable: false),
                        TypeReglement = c.Int(nullable: false),
                        DateCreation = c.DateTime(),
                        DateModification = c.DateTime(),
                    })
                .PrimaryKey(t => t.AbonnementId)
                .ForeignKey("dbo.Adherents", t => t.AdherentId)
                .ForeignKey("dbo.Formules", t => t.FormuleId)
                .Index(t => new { t.AdherentId, t.FormuleId }, unique: true, name: "AK_Abo_AdherentFormule");
            
            CreateTable(
                "dbo.Adherents",
                c => new
                    {
                        AdherentId = c.Int(nullable: false, identity: true),
                        AdherentNom = c.String(nullable: false, maxLength: 30),
                        AdherentPrenom = c.String(nullable: false, maxLength: 30),
                        Famille = c.String(maxLength: 30),
                        Notes = c.String(maxLength: 150),
                        EMail = c.String(maxLength: 80),
                        Telephone = c.String(maxLength: 10),
                        Adresse = c.String(maxLength: 150),
                        Adresse2 = c.String(maxLength: 150),
                        CodePostal = c.String(maxLength: 5),
                        Ville = c.String(maxLength: 150),
                        DateCreation = c.DateTime(),
                        DateModification = c.DateTime(),
                        DateResiliation = c.DateTime(),
                        Statut = c.Int(nullable: false),
                        CertificatMedical = c.Boolean(),
                        AttestationAssurance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdherentId)
                .Index(t => new { t.AdherentNom, t.AdherentPrenom }, name: "AK_Adherent_AdherentNom");
            
            CreateTable(
                "dbo.Formules",
                c => new
                    {
                        FormuleId = c.Int(nullable: false, identity: true),
                        FormuleNom = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(),
                        Tarif = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActiviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormuleId)
                .ForeignKey("dbo.Activites", t => t.ActiviteId)
                .Index(t => t.FormuleNom, name: "AK_Formule_FormuleNom")
                .Index(t => t.ActiviteId);
            
            CreateTable(
                "dbo.Activites",
                c => new
                    {
                        ActiviteId = c.Int(nullable: false, identity: true),
                        ActiviteNom = c.String(nullable: false, maxLength: 100),
                        ActiviteCode = c.String(nullable: false, maxLength: 15),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        PlanifJour = c.Int(nullable: false),
                        PlanfHeureDebut = c.Time(nullable: false, precision: 7),
                        PlanfHeureFin = c.Time(nullable: false, precision: 7),
                        LieuId = c.Int(nullable: false),
                        CategorieActiviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActiviteId)
                .ForeignKey("dbo.CategorieActivites", t => t.CategorieActiviteId)
                .ForeignKey("dbo.Lieux", t => t.LieuId)
                .Index(t => t.ActiviteCode, unique: true, name: "AK_Activite_ActiviteCode")
                .Index(t => t.LieuId, name: "AK_Activite_ActiviteLieu")
                .Index(t => t.CategorieActiviteId);
            
            CreateTable(
                "dbo.CategorieActivites",
                c => new
                    {
                        CategorieActiviteId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        CategorieActiviteNom = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategorieActiviteId)
                .ForeignKey("dbo.CategorieActivites", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.CategorieActiviteNom, unique: true, name: "AK_CategorieActivite_CategorieActiviteNom");
            
            CreateTable(
                "dbo.Lieux",
                c => new
                    {
                        LieuId = c.Int(nullable: false, identity: true),
                        LieuNom = c.String(nullable: false, maxLength: 100),
                        LieuCode = c.String(nullable: false, maxLength: 15),
                        Adresse = c.String(maxLength: 150),
                        Adresse2 = c.String(maxLength: 150),
                        CodePostal = c.String(maxLength: 5),
                        Ville = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.LieuId)
                .Index(t => t.LieuNom, unique: true, name: "AK_Lieu_LieuNom")
                .Index(t => t.LieuCode, unique: true, name: "AK_Lieu_LieuCode");
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        MissionId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 80),
                        Notes = c.String(maxLength: 200),
                        SalaireHoraire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        IntervenantId = c.Int(nullable: false),
                        ActiviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MissionId)
                .ForeignKey("dbo.Activites", t => t.ActiviteId)
                .ForeignKey("dbo.Intervenants", t => t.IntervenantId)
                .Index(t => t.IntervenantId)
                .Index(t => t.ActiviteId);
            
            CreateTable(
                "dbo.Intervenants",
                c => new
                    {
                        IntervenantId = c.Int(nullable: false, identity: true),
                        IntervenantNom = c.String(nullable: false, maxLength: 50),
                        IntervenantPrenom = c.String(nullable: false, maxLength: 50),
                        NumeroSecuriteSociale = c.String(maxLength: 15),
                        DateCreation = c.DateTime(),
                        DateModification = c.DateTime(),
                    })
                .PrimaryKey(t => t.IntervenantId)
                .Index(t => t.IntervenantNom, name: "AK_Intervenant_IntervenantNom");
            
            CreateTable(
                "dbo.Reglements",
                c => new
                    {
                        ReglementId = c.Int(nullable: false, identity: true),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoyenPaiement = c.Int(nullable: false),
                        IsAdhesionIncluse = c.Boolean(nullable: false),
                        ChequeNumero = c.String(maxLength: 10),
                        ChequeBanque = c.String(maxLength: 80),
                        ChequeTitulaire = c.String(maxLength: 80),
                        ChequeDate = c.DateTime(),
                        ChequeDateEncaissement = c.DateTime(),
                        AbonnementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReglementId)
                .ForeignKey("dbo.Abonnements", t => t.AbonnementId)
                .Index(t => t.ChequeNumero, name: "AK_Cheque_ChequeNumero")
                .Index(t => t.AbonnementId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 15),
                        LastName = c.String(maxLength: 15),
                        Address = c.String(maxLength: 30),
                        City = c.String(maxLength: 30),
                        ZipCode = c.String(maxLength: 5),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reglements", "AbonnementId", "dbo.Abonnements");
            DropForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants");
            DropForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Activites", "LieuId", "dbo.Lieux");
            DropForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites");
            DropForeignKey("dbo.CategorieActivites", "ParentId", "dbo.CategorieActivites");
            DropForeignKey("dbo.Abonnements", "FormuleId", "dbo.Formules");
            DropForeignKey("dbo.Abonnements", "AdherentId", "dbo.Adherents");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reglements", new[] { "AbonnementId" });
            DropIndex("dbo.Reglements", "AK_Cheque_ChequeNumero");
            DropIndex("dbo.Intervenants", "AK_Intervenant_IntervenantNom");
            DropIndex("dbo.Missions", new[] { "ActiviteId" });
            DropIndex("dbo.Missions", new[] { "IntervenantId" });
            DropIndex("dbo.Lieux", "AK_Lieu_LieuCode");
            DropIndex("dbo.Lieux", "AK_Lieu_LieuNom");
            DropIndex("dbo.CategorieActivites", "AK_CategorieActivite_CategorieActiviteNom");
            DropIndex("dbo.CategorieActivites", new[] { "ParentId" });
            DropIndex("dbo.Activites", new[] { "CategorieActiviteId" });
            DropIndex("dbo.Activites", "AK_Activite_ActiviteLieu");
            DropIndex("dbo.Activites", "AK_Activite_ActiviteCode");
            DropIndex("dbo.Formules", new[] { "ActiviteId" });
            DropIndex("dbo.Formules", "AK_Formule_FormuleNom");
            DropIndex("dbo.Adherents", "AK_Adherent_AdherentNom");
            DropIndex("dbo.Abonnements", "AK_Abo_AdherentFormule");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reglements");
            DropTable("dbo.Intervenants");
            DropTable("dbo.Missions");
            DropTable("dbo.Lieux");
            DropTable("dbo.CategorieActivites");
            DropTable("dbo.Activites");
            DropTable("dbo.Formules");
            DropTable("dbo.Adherents");
            DropTable("dbo.Abonnements");
        }
    }
}
