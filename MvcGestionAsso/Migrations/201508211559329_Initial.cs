namespace MvcGestionAsso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activites",
                c => new
                    {
                        ActiviteId = c.Int(nullable: false, identity: true),
                        ActiviteNom = c.String(nullable: false, maxLength: 100),
                        ActiviteCode = c.String(nullable: false, maxLength: 15),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        DureeHeures = c.Single(nullable: false),
                        LieuId = c.Int(nullable: false),
                        CategorieActiviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActiviteId)
                .ForeignKey("dbo.CategorieActivites", t => t.CategorieActiviteId, cascadeDelete: true)
                .ForeignKey("dbo.Lieux", t => t.LieuId, cascadeDelete: true)
                .Index(t => t.ActiviteNom, unique: true, name: "AK_Activite_ActiviteNom")
                .Index(t => t.ActiviteCode, unique: true, name: "AK_Activite_ActiviteCode")
                .Index(t => t.LieuId)
                .Index(t => t.CategorieActiviteId);
            
            CreateTable(
                "dbo.CategorieActivites",
                c => new
                    {
                        CategorieActiviteId = c.Int(nullable: false, identity: true),
                        CategorieActiviteNom = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategorieActiviteId)
                .Index(t => t.CategorieActiviteNom, unique: true, name: "AK_CategorieActivite_CategorieActiviteNom");
            
            CreateTable(
                "dbo.Lieux",
                c => new
                    {
                        LieuId = c.Int(nullable: false, identity: true),
                        LieuCode = c.String(nullable: false, maxLength: 15),
                        LieuNom = c.String(nullable: false, maxLength: 100),
                        Adresse = c.String(maxLength: 150),
                        Adresse2 = c.String(maxLength: 150),
                        CodePostal = c.String(maxLength: 5),
                        Ville = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.LieuId)
                .Index(t => t.LieuCode, unique: true, name: "AK_Lieu_LieuCode")
                .Index(t => t.LieuNom, unique: true, name: "AK_Lieu_LieuNom");
            
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
                .ForeignKey("dbo.Activites", t => t.ActiviteId, cascadeDelete: true)
                .ForeignKey("dbo.Intervenants", t => t.IntervenantId, cascadeDelete: true)
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
                        DateCreation = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
												DateModification = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    })
                .PrimaryKey(t => t.IntervenantId)
                .Index(t => t.IntervenantNom, name: "AK_Intervenant_IntervenantNom");
            
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
												DateCreation = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
												DateModification = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        DateResiliation = c.DateTime(),
                        Statut = c.Int(nullable: false),
                        CertificatMedical = c.Boolean(),
                    })
                .PrimaryKey(t => t.AdherentId)
                .Index(t => t.AdherentNom, name: "AK_Adherent_AdherentNom");
            
            CreateTable(
                "dbo.Formules",
                c => new
                    {
                        FormuleId = c.Int(nullable: false, identity: true),
                        FormuleNom = c.String(nullable: false, maxLength: 50),
                        DebutValidite = c.DateTime(nullable: false),
                        FinValidite = c.DateTime(nullable: false),
                        IsActive = c.Boolean(),
                        Tarif = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActiviteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormuleId)
                .ForeignKey("dbo.Activites", t => t.ActiviteId, cascadeDelete: true)
                .Index(t => t.FormuleNom, unique: true, name: "AK_Formule_FormuleNom")
                .Index(t => t.ActiviteId);
            
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
                        AdherentId = c.Int(nullable: false),
                        FormuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReglementId)
                .ForeignKey("dbo.Adherents", t => t.AdherentId, cascadeDelete: true)
                .ForeignKey("dbo.Formules", t => t.FormuleId, cascadeDelete: true)
                .Index(t => t.ChequeNumero, unique: true, name: "AK_Cheque_ChequeNumero")
                .Index(t => t.AdherentId)
                .Index(t => t.FormuleId);
            
            CreateTable(
                "dbo.ModeReglements",
                c => new
                    {
                        ModeReglementId = c.Int(nullable: false, identity: true),
                        ModeReglementCode = c.String(nullable: false, maxLength: 15),
                        ModeReglementNom = c.String(nullable: false, maxLength: 100),
                        AdherentId = c.Int(nullable: false),
                        FormuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeReglementId)
                .ForeignKey("dbo.Adherents", t => t.AdherentId, cascadeDelete: true)
                .ForeignKey("dbo.Formules", t => t.FormuleId, cascadeDelete: true)
                .Index(t => t.ModeReglementCode, unique: true, name: "AK_ModeReglement_ModeReglementCode")
                .Index(t => t.ModeReglementNom, unique: true, name: "AK_ModeReglement_ModeReglementNom")
                .Index(t => t.AdherentId)
                .Index(t => t.FormuleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FormuleAdherents",
                c => new
                    {
                        Formule_FormuleId = c.Int(nullable: false),
                        Adherent_AdherentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Formule_FormuleId, t.Adherent_AdherentId })
                .ForeignKey("dbo.Formules", t => t.Formule_FormuleId, cascadeDelete: true)
                .ForeignKey("dbo.Adherents", t => t.Adherent_AdherentId, cascadeDelete: true)
                .Index(t => t.Formule_FormuleId)
                .Index(t => t.Adherent_AdherentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ModeReglements", "FormuleId", "dbo.Formules");
            DropForeignKey("dbo.ModeReglements", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.Reglements", "FormuleId", "dbo.Formules");
            DropForeignKey("dbo.Reglements", "AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.FormuleAdherents", "Adherent_AdherentId", "dbo.Adherents");
            DropForeignKey("dbo.FormuleAdherents", "Formule_FormuleId", "dbo.Formules");
            DropForeignKey("dbo.Formules", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Missions", "IntervenantId", "dbo.Intervenants");
            DropForeignKey("dbo.Missions", "ActiviteId", "dbo.Activites");
            DropForeignKey("dbo.Activites", "LieuId", "dbo.Lieux");
            DropForeignKey("dbo.Activites", "CategorieActiviteId", "dbo.CategorieActivites");
            DropIndex("dbo.FormuleAdherents", new[] { "Adherent_AdherentId" });
            DropIndex("dbo.FormuleAdherents", new[] { "Formule_FormuleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ModeReglements", new[] { "FormuleId" });
            DropIndex("dbo.ModeReglements", new[] { "AdherentId" });
            DropIndex("dbo.ModeReglements", "AK_ModeReglement_ModeReglementNom");
            DropIndex("dbo.ModeReglements", "AK_ModeReglement_ModeReglementCode");
            DropIndex("dbo.Reglements", new[] { "FormuleId" });
            DropIndex("dbo.Reglements", new[] { "AdherentId" });
            DropIndex("dbo.Reglements", "AK_Cheque_ChequeNumero");
            DropIndex("dbo.Formules", new[] { "ActiviteId" });
            DropIndex("dbo.Formules", "AK_Formule_FormuleNom");
            DropIndex("dbo.Adherents", "AK_Adherent_AdherentNom");
            DropIndex("dbo.Intervenants", "AK_Intervenant_IntervenantNom");
            DropIndex("dbo.Missions", new[] { "ActiviteId" });
            DropIndex("dbo.Missions", new[] { "IntervenantId" });
            DropIndex("dbo.Lieux", "AK_Lieu_LieuNom");
            DropIndex("dbo.Lieux", "AK_Lieu_LieuCode");
            DropIndex("dbo.CategorieActivites", "AK_CategorieActivite_CategorieActiviteNom");
            DropIndex("dbo.Activites", new[] { "CategorieActiviteId" });
            DropIndex("dbo.Activites", new[] { "LieuId" });
            DropIndex("dbo.Activites", "AK_Activite_ActiviteCode");
            DropIndex("dbo.Activites", "AK_Activite_ActiviteNom");
            DropTable("dbo.FormuleAdherents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ModeReglements");
            DropTable("dbo.Reglements");
            DropTable("dbo.Formules");
            DropTable("dbo.Adherents");
            DropTable("dbo.Intervenants");
            DropTable("dbo.Missions");
            DropTable("dbo.Lieux");
            DropTable("dbo.CategorieActivites");
            DropTable("dbo.Activites");
        }
    }
}
