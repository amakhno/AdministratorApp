namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUpdateAfterTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingRooms", "CountOfChairs", c => c.Int(nullable: false));
            AddColumn("dbo.Meetings", "MeetingRoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Meetings", "MeetingRoomId");
            AddForeignKey("dbo.Meetings", "MeetingRoomId", "dbo.MeetingRooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "MeetingRoomId", "dbo.MeetingRooms");
            DropIndex("dbo.Meetings", new[] { "MeetingRoomId" });
            DropColumn("dbo.Meetings", "MeetingRoomId");
            DropColumn("dbo.MeetingRooms", "CountOfChairs");
        }
    }
}
