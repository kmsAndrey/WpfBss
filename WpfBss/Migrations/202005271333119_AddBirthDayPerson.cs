namespace WpfBss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthDayPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "BirthDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "BirthDay");
        }
    }
}
