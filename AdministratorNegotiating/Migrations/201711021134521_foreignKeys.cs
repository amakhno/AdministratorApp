namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Meetings", "NameOfMeeting", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Meetings", "UserName");
            AddForeignKey("dbo.Meetings", "UserName", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Meetings", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Meetings", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Meetings", new[] { "UserName" });
            AlterColumn("dbo.Meetings", "NameOfMeeting", c => c.Int(nullable: false));
            DropColumn("dbo.Meetings", "UserName");
        }
    }
}
