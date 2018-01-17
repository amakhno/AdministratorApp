namespace AdministratorNegotiating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moneyadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Money");
        }
    }
}
