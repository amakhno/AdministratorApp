namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class password : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meetings", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meetings", "Discriminator");
        }
    }
}
