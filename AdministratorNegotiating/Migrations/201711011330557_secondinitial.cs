namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondinitial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingRooms", "Name", c => c.String());
            AddColumn("dbo.MeetingRooms", "IsBoard", c => c.Boolean(nullable: false));
            AddColumn("dbo.MeetingRooms", "IsProjector", c => c.Boolean(nullable: false));
            AddColumn("dbo.Meetings", "NameOfMeeting", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "NameOfMeeting");
            DropColumn("dbo.MeetingRooms", "IsProjector");
            DropColumn("dbo.MeetingRooms", "IsBoard");
            DropColumn("dbo.MeetingRooms", "Name");
        }
    }
}
