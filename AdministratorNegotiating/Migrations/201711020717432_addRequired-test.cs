namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequiredtest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meetings", "MeetingRoom_Id", "dbo.MeetingRooms");
            DropIndex("dbo.Meetings", new[] { "MeetingRoom_Id" });
            DropColumn("dbo.Meetings", "MeetingRoom_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "MeetingRoom_Id", c => c.Int());
            CreateIndex("dbo.Meetings", "MeetingRoom_Id");
            AddForeignKey("dbo.Meetings", "MeetingRoom_Id", "dbo.MeetingRooms", "Id");
        }
    }
}
