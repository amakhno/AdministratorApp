namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAnnotationForMeetingRoom : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MeetingRooms", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.MeetingRooms", "CountOfChairs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MeetingRooms", "CountOfChairs", c => c.Int(nullable: false));
            AlterColumn("dbo.MeetingRooms", "Name", c => c.String());
        }
    }
}
