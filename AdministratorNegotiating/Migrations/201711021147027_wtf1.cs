namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meetings", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Meetings", new[] { "UserName" });
            AddColumn("dbo.Meetings", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Meetings", "UserName", c => c.String());
            CreateIndex("dbo.Meetings", "User_Id");
            AddForeignKey("dbo.Meetings", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Meetings", new[] { "User_Id" });
            AlterColumn("dbo.Meetings", "UserName", c => c.String(maxLength: 128));
            DropColumn("dbo.Meetings", "User_Id");
            CreateIndex("dbo.Meetings", "UserName");
            AddForeignKey("dbo.Meetings", "UserName", "dbo.AspNetUsers", "Id");
        }
    }
}
