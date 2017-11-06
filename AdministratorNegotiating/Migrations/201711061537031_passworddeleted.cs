namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passworddeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meetings", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meetings", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
