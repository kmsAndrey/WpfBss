namespace WpfBss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalCard = c.String(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FIO = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        INN = c.String(),
                        Person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Clients", "Person_Id", "dbo.People");
            DropIndex("dbo.Employees", new[] { "Person_Id" });
            DropIndex("dbo.Clients", new[] { "Person_Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.People");
            DropTable("dbo.Clients");
        }
    }
}
