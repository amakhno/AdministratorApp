namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meetingprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "Price");
        }
    }
}
